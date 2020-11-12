using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpdrachtWeekDrie.Models;

/*Definieer in de constructor een vaste lijst met een aantal Studenten met unieke studentnummers, uniek emailadressen en soms dezelfde voornaam 
 * (over een paar weken gebruiken we een database i.p.v. deze lijst). Schrijf drie Actions:
De Action Aantal heeft als parameter voornaam en toont "De naam Jan komt {n} keer voor in de lijst." in de browser, 
waarbij n het aantal studenten is met voornaam voornam.
De Action Email heeft als parameter een getal, bijvoorbeeld 1897836 (de url eindigt met /1897836),
en geeft dan het emailadres van de student met studentnummer 1897836 in de lijst terug.
Zorg dat als het studentnummer niet bestaat dat er een gebruikersvriendelijke boodschap wordt gegeven dat deze student niet in de lijst voorkomt.
De Action ZoekStudenten heeft als parameter een letter en toont de lijst met studenten waarvan de voornaam begint met die letter 
(dus te benaderen via de url localhost/StudentAdministratie/ZoekStudenten/j voor de letter j).
Pas het MVC-pattern toe door vanuit de Controller een Model mee te geven aan de View.
*/
namespace OpdrachtWeekDrie.Controllers
{
    public class StudentController : Controller
    {
        static List<Student> lijst = new List<Student>();
        public StudentController()
        {
            if (lijst.Count == 0)
            {
                lijst.Add(new Student(12314300, "Eva"));
                lijst.Add(new Student(12314301, "Bert"));
                lijst.Add(new Student(12314302, "Bert"));
                lijst.Add(new Student(12314303, "Maria"));
                lijst.Add(new Student(12123123, "Customia", "customemail@domain.com"));
            }
        }
        public IActionResult Index()
        {
            lijst.Add(new Student(12314300, "Eva"));
            return View();
        }

        public IActionResult Student()
        {
            return View(lijst);
        }
        /*
        De Action Email heeft als parameter een getal, bijvoorbeeld 1897836 (de url eindigt met /1897836),
en geeft dan het emailadres van de student met studentnummer 1897836 in de lijst terug.
Zorg dat als het studentnummer niet bestaat dat er een gebruikersvriendelijke boodschap wordt gegeven dat deze student niet in de lijst voorkomt. */
        public IActionResult Email(int studentnummer) {
            string antwoord;
            if (lijst.Exists(x => x.studentNummer == studentnummer))
            {
                Student gevonden = lijst.Find(
                    delegate (Student s)
                    {
                        return s.studentNummer == studentnummer;
                    });
              
                antwoord = "Het emailadres voor de student  " + gevonden.Voornaam + " is " + gevonden.email;
                ViewData["Bericht"] = antwoord;
            }
            else {
                antwoord = "Er zijn geen studenten met studentnummer " + studentnummer + " gevonden.";
                ViewData["Bericht"] = antwoord;
            }
           
            return View();  
        }

        /*De Action ZoekStudenten heeft als parameter een letter en toont de lijst met studenten waarvan de voornaam begint met die letter
(dus te benaderen via de url localhost/StudentAdministratie/ZoekStudenten/j voor de letter j).
        eigen view so it means eigen controller? */

       public IActionResult ZoekStudenten(char letter)
        {
            
            List<Student> begintmet = new List<Student>();
            foreach (Student s in lijst)
            {
                letter = Char.ToUpper(letter);
                if (s.Voornaam.StartsWith(letter)) {
                    begintmet.Add(s);
                }
            }
            if (begintmet.Count == 0) {
                ViewData["Zoek"] = "Er zijn geen studenten gevonden waarvan de naam met de letter " + letter + " begint. ";
                return View();
            }
            else {
                //ViewData["Zoek"] = "De studenten in de lijst met beginletter " + letter + " zijn " + "@foreach(Student s in begintmet){ < div >  @s.Voornaam  </ div > };";
                return View(begintmet);
            }
        }
      
        /* 
        De Action Aantal heeft als parameter voornaam en toont "De naam Jan komt {n} keer voor in de lijst." in de browser, 
    waarbij n het aantal studenten is met voornaam voornam.*/

        public IActionResult Aantal(string voornam)
            {
            int occurance=0;
            string antwoord;
            foreach (Student astudent in lijst)
            {
                if (astudent.Voornaam == voornam) 
                {
                    occurance += 1;
                }
            }
            antwoord = "De naam " + voornam + " komt " + occurance + " keer voor";
            ViewData["Message"] = antwoord;
            return View();
            }
        } 
    }

