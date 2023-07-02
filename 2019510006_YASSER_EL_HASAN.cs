using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Assignment_Firas
{
    class Program
    {

        static void Main(string[] args)
        {

            string[] poem = File.ReadAllLines(@"E:\poem.txt");

            //checking the same sounds
            similar_sounds(poem);

            //Check by repeating arrangmenets
            //CheckByArrangment(poem);

        }


        private static void similar_sounds(string[] poem)
        {
            //kins of rhymes
            additinal_rhymes(poem);
            similar_words(poem);
            phrases(poem);


        }

        private static bool additinal_rhymes(string[] poem)
        {
            //Get all words
            List<string> wordsList = new List<string>();


            foreach (string line in poem)
            {
                //Separate all words into an array
                string[] words = line.Split(' ');

                //Get last word
                string lastWord = words[words.Length - 1];

                wordsList.Add(lastWord);

            }


            List<string> isTheSame_letters = new List<string>();

            for (int i = 0; i < wordsList.Count; i++)
            {
                // If we reached the last then break since we can't compare any more.
                if (i == wordsList.Count - 1)
                    break;

                string firstWord = wordsList[i];
                string nextWord = wordsList[i + 1];


                // Compare words from the last letters; if we have a rhyme then retrieve the letters into this variable
                string retrievedLetters = checkLettersInWords(firstWord, nextWord);
                if (retrievedLetters.Length > 0)
                    isTheSame_letters.Add(retrievedLetters);


            }

            // Create a hashset to remove duplicates
            HashSet<string> isTheSame_lettersSet = new HashSet<string>(isTheSame_letters.ToArray());




            if (isTheSame_lettersSet.Count == 1)
            {
                Console.WriteLine("Repetition " + isTheSame_letters[0]);
                Console.WriteLine("Type: Additional Rhyme");
                return true;
            }
            return false;

        }

        private static bool phrases(string[] poem)
        {
            // All lines from the poem will be saved here
            List<string> lines = new List<string>(poem);

            // The final phrases will be saved in this list of lists
            List<List<string>> phrases = new List<List<string>>();


            for (int i = 0; i < lines.Count - 1; i++)
            {
                // Get current line
                string line = lines[i];

                // Get next line
                string nextLine = lines[i + 1];

                // Compare words after splitting them into string arrays
                List<string> phrase = CompareWords(line.Split(' '), nextLine.Split(' '));

                // If there are 1 or more words in this phrases then add to list of phrases
                if (phrase.Count > 0)
                    phrases.Add(phrase);


            }

            // If count of phrases is equal to or more than one then we have match phrases in this poem
            if (phrases.Count >= 1)
            {
                Console.WriteLine("Repetition:");

                // List all match phrases
                foreach (List<string> phrase in phrases)
                    Console.WriteLine(string.Join(" ", phrase.ToArray()));

                Console.WriteLine("Type: Additional Rhyme");
                return true;
            }


            return false;

        }

        private static List<string> CompareWords(string[] line, string[] nextLine)
        {
            List<string> similarPhrase = new List<string>();
            for (int i = 1; i < line.Length; i++)
            {
                // Getting the current word
                string firstWord = line[line.Length - i];

                // Getting the second word
                string secondWord = nextLine[nextLine.Length - i];

                // Check if both words match
                bool res = firstWord.Equals(secondWord);


                // If they match then insert them at the end of the list to get the correct 
                // structure of the poem's phrase
                if (res)
                    similarPhrase.Insert(similarPhrase.Count, firstWord);

            }

            return similarPhrase;

        }

        private static bool similar_words(string[] poem)
        {
            List<string> wordsList = new List<string>();


            foreach (string line in poem)
            {
                //storing words in an array
                string[] words = line.Split(' ');

                //reaching the last word
                string lastWord = words[words.Length - 1];

                //importing words to list
                wordsList.Add(lastWord);

            }

            //  removing all duplicates 
            HashSet<string> wordsSet = new HashSet<string>(wordsList);

            // checking how many words we have to determine the repetion
            if (wordsSet.Count == 1)
            {
                Console.WriteLine("Repetition " + wordsList[0]);
                Console.WriteLine("Type: Word Rhyme");
                return true;
            }

            return false;
        }

       

        private static string checkLettersInWords(string firstWord, string nextWord)
        {
            // determine the longets word.
            int biggestSize = firstWord.Length > nextWord.Length ? firstWord.Length : nextWord.Length;

            // is the first word the bigger one?
            bool firstWordBig = firstWord.Length > nextWord.Length ? true : false;


            
            bool isTheSame = false;

            // cheking the letter position.
            int letterPosition = 0;
            while (!isTheSame)
            {
                bool match;

                // catching the bigest word and comparing it with the othors.
                //  or doing the oposite
                if (firstWordBig)
                    match = firstWord.EndsWith(nextWord.Substring(nextWord.Length - letterPosition));
                else
                    match = nextWord.EndsWith(firstWord.Substring(firstWord.Length - letterPosition));


                
                if (match)
                    letterPosition++;
                else
                    letterPosition--;

                if (biggestSize - 1 == letterPosition)
                {
                    isTheSame = true;
                    letterPosition--;
                }

                if (!match)
                    isTheSame = true;
            }

            string similarLetter;
            // checking the same letters
            if (firstWordBig)
            {
                similarLetter = firstWord.Substring(firstWord.Length - letterPosition);
            }
            else
            {
                similarLetter = nextWord.Substring(nextWord.Length - letterPosition);

            }

            
            return similarLetter;
        }
    }
}
