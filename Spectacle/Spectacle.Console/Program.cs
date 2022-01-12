// Acteurs
using Spectacle.Application.Models;

var spectateur = new Spectateur("spectateur", new ConsoleDisplay());
var singe_1 = new Singe("1");
var singe_2 = new Singe("2");
var dresseur_1 = new Dresseur(singe_1);
var dresseur_2 = new Dresseur(singe_2);

// Assigner des tours aux singes
singe_1.AjouterTour(new TourAcrobatie("marcher sur les mains"));
singe_1.AjouterTour(new TourMusique("jouer du ukulele"));

singe_2.AjouterTour(new TourAcrobatie("faire des pirouettes"));
singe_2.AjouterTour(new TourMusique("jouer des cymbales"));

// Les dresseurs acceuillent le spectateur
dresseur_1.AcceuilleSpectateur(spectateur);
dresseur_2.AcceuilleSpectateur(spectateur);

// dresseurs démarrent le spectable
dresseur_1.DemarreSpectacle();
dresseur_2.DemarreSpectacle();