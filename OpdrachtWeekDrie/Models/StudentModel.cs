using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpdrachtWeekDrie.Models
{
    public class Student
    {
        public Student(int studentnummer, string voornaam) {
            studentNummer = studentnummer;
            Voornaam = voornaam;
            email = emailGen(studentnummer);
        }
        public Student(int studentnummer, string voornaam, string mailie)
        {
            studentNummer = studentnummer;
            Voornaam = voornaam;
            email = mailie;
        }

        public int studentNummer { get; set; }
        public string Voornaam { get; set; }
        public string email { get; set; }

        internal string emailGen(int studentnummer) {
            string mail;
            mail = studentnummer + "@student.hhs.nl";
            return mail;
        }
    }
}
