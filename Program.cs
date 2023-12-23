using System.Collections.Concurrent;
using System.IO;

namespace Wordle
{
    class WordleGame {         
        static void Main(string[] args)
        {

            Random random = new Random();

            string[] wordList = readLines().ToArray();

            
            string in_word = "";
            string answerWord = wordList[random.Next(0, wordList.Length-1)];
            int guesses = 0;
            bool correct = false;
            List<char> wrongLetters = new List<char>();
            Dictionary<char,int> charsInWord = new Dictionary<char,int>();

            //Maps each char in answer string to the number of times it appear in the string
            foreach(char letter in answerWord)
            {
                charsInWord[letter] = charsInWord.GetValueOrDefault(letter, 0) + 1;
            }


            while (in_word != answerWord && guesses != 6){
                
                //If list is empty print every letter user has guessed
                if (wrongLetters.Any()) {
                    Console.WriteLine(getWrongChars(wrongLetters));
                }



                Console.WriteLine("Enter a word");
                in_word = Console.ReadLine().ToLower();

                Dictionary<char, int> userInChars = new Dictionary<char, int>();

                //If word is not in word list 
                if (!wordList.Contains(in_word)) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Not a valid word");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                };

                    //Prints letters with colour corresponding to if they are in the correct position
                    for(int i=0; i < 5; i++){
                        if(in_word[i] == answerWord[i]){
                            Console.ForegroundColor = ConsoleColor.Green;
                            userInChars[in_word[i]] = userInChars.GetValueOrDefault(in_word[i], 0) + 1;
                        }
                        else if (answerWord.Contains(in_word[i]) && userInChars.GetValueOrDefault(in_word[i], 0) < charsInWord[in_word[i]]){
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        else{
                            Console.ForegroundColor = ConsoleColor.Red;
                            if (!wrongLetters.Contains(in_word[i])) wrongLetters.Add(in_word[i]);
                        }

                        Console.Write(in_word[i]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                 


                if (in_word == answerWord) correct = true;

            Console.Write('\n');
            guesses++;
        }


            if (correct) {
                Console.WriteLine("You got it!");
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Better luck next time");
            }

            Console.WriteLine("The word was " + answerWord);

            

            Console.ReadKey();


            
        }

        //Reads all characters except for plurals
        static List<string> readLines(){
            using(var reader = new StreamReader("./sgb-words.txt")){
                List<string> wordList = new List<string>();

                while(!reader.EndOfStream){
                    var line = reader.ReadLine();

                    if (line[line.Length-1] != 's') wordList.Add(line);
                }

                return wordList;
            }

        }

       static string getWrongChars(List<char> wrongWords){

            wrongWords.Sort();
            string out_string = "Letters not in word so far ";
            foreach(char letter in wrongWords){
                out_string += letter + ", ";
            }


        return out_string.Substring(0, out_string.Length -2);
        } 

    }
}