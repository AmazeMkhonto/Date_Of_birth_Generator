﻿using System;
using System.IO;
using System.Collections.Generic;

namespace IDNumbers
{
    class Program
    {


        static void Main(string[] args) {
            // if (args.Length == 0){
            //     Console.WriteLine("Please provide a path to the text file.");
            //     return;
            // }

            // string filePath = args[0];

            int bornBefore2010 = 0;
            int bornAfter2010 = 0;


            string filePath = @"idNumbers.txt";
            List<DateTime> datesOfBirth = BirthDate(filePath);

            foreach (DateTime dateOfBirth in datesOfBirth) {
                Console.WriteLine(dateOfBirth.ToString("dd-MM-yyyy"));

                if (dateOfBirth.Year < 2010){
                    bornBefore2010++;
                } else {
                    bornAfter2010++;
                }
            }
             ageCount(bornBefore2010, bornAfter2010);
            
        }


        static DateTime BirthDateFormat(string idNumber)
        {
            int year = int.Parse(idNumber.Substring(0, 2));
            int month = int.Parse(idNumber.Substring(2, 2));
            int day = int.Parse(idNumber.Substring(4, 2));

            if (year >= 0 && year <= 20){
                year += 2000;
            } else {
                year += 1900;
            }
            DateTime format = new DateTime(year, month, day);
            return format;
        }




        static List<DateTime> BirthDate(string filePath) {

            List<DateTime> datesOfBirth = new List<DateTime>();
            try
            {
                string[] idNumbers = File.ReadAllLines(filePath);

                foreach (string idNumber in idNumbers)
                {
                    if (idNumber.Length != 13)
                    //TODO: Check that id does not have letters if its 13 character
                    // or contain any spaces, actually check if it only has digits
                    // should we have duplicate id numbers
                    {
                        Console.WriteLine($"Error: ID number {idNumber} is not 13 digits.");
                        continue;
                    }

                    DateTime dateOfBirth = BirthDateFormat(idNumber);
                    datesOfBirth.Add(dateOfBirth);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }

            return datesOfBirth;
        }



         static void ageCount(int before2010, int after2010)
         {
            string filePath = "ageCount.txt";
                string[] lines = { $"People born before 2010: {before2010}",
                                   $"People born after 2010: {after2010}"};
                File.WriteAllLines(filePath, lines);
        }

        
    }
}