# Test-technique-Stage-VR-Studio-LDLC
Le test technique que demande LDLC VR studio pour tous les candidats au stage pour le poste de "Développeur.euse Unity Jeu Vidéo en VR"

Date limite d'envoi : **mardi 21 janvier 23h59** (un retour anticipé est bien sûr possible).
# Information supplémentaire 
## Etapes de développement
## Temps passé
* jeudi : 1h
* vendredi : 3h
* samedi : 2h
* dimanche : 4h
* lundi : 3h
* mardi : 3h

total = 3 * 3 + 2 + 4 + 1 = 16 h
## choix importants
## difficultés rencontrées
## Passage en VR
### En terme technique
### En terme d'expérience

# Structure du code
# Les scènes
## Basic Gameplay
La possibilité de pouvoir sélectionner un objet = cube devient jaune lorsque l'on est dans la zone ou l'on peut déposer
La possibilité de pouvoir déposer un objet = cube devient bleu lorsque l'on est dans la zone ou l'on peut déposer
## Tuned Gameplay
## VR Gameplay

# TODO
## Basic Gameplay
- [x] Character Controller
    - [x] déplacements avant / arrière – gauche / droite
    - [x] contrôle de la vue à la souris
- [x] interagir avec les objets
    - [x] Clic gauche = prendre objet (le faire apparaitre dans une des 2 mains)
    - [x] Clic droit = lacher objet
    - [x] ajouter les feedback pour prendre l'objet ou lorsque l'on est a une certaine distance + la souris pointe sur l'objet alors feedback **jaune**
    - [x] ajouter les feedback pour lacher l'objet ou lorsque l'on dans la zone du conteneur avec un objet cette objet qui est dans les main apparait en **bleu**
- [x] Elément de gameplay
    - [x] temps imparti
        - [x] mettre un chrono en haut au milieu de la vue du joueur
        - [x] mettre un compteur d'objet sur la vue de l'utilisateur
    - [x] ecran de fin
        - [x] game Over (affichant le nombre d’éléments restants qui n’ont pas été déplacés à temps)
        - [x] Bravo !
- [x] faire un scriptable object pour pouvoir modifier : 
    - [x] Temps de jeu
    - [x] Nombre de pièces à déplacer pour gagner la partie 
- [x]  configuration avec un Custom Editor qui présente de manière agréable l’édition du 
scriptableobject, à destination de l’équipe design.

## Tuned Gameplay

Idées :
- Shaders : ?
- particule : on peut mettre des confettis lorsque la personne a gagné
- [PostFX](https://www.youtube.com/watch?v=9tjYz6Ab0oc&ab_channel=Brackeys) : pourquoi pas au moment du game over on met l'écran en noir et blanc
- Modèles / textures 
    - soit on fait comme si on était a un match de basket
    - soit on fait comme si on était au bureau et on jette des boule de papiers
- Audio 
    - mettre un son d'ambiance réaliste (match basket ou bureau) ou juste une musique
    - mettre un son a chaque lancé réussi(ouai !) et chaque échec (oohh)
    - mettre un son de marche
    - mettre une musique ou un changement d'ambiance pour l'écran de fin selon le résultat
- Idées de gameplay complémentaire
    - pouvoir jeter la balle
    - bloquer le joueur dans un terrain délimité
    - le conteneur bouge ou change de place apres chaque lancé réussi
    - faire 3 niveau ou l'on augmente la difficulté à chaque fois
        - easy = le conteneur ne bouge pas
        - medium le conteneur se téléporte a chaque lancé réussi
        - hard le conteneur bouge avec un certain pattern 
## VR Gameplay 

# Feuille de route
* Jeudi : mise en place du git et je commence a planifier / réfléchir à l'implémentation
* Vendredi : je commence le code pour faire le gameplay basic qui est demandé en en faisant le stricte minimum.
* Samedi : selon comment j'ai avancé le gameplay basic soit je le continu soit à partir de la scene basique je crée la scene "tuned gameplay" ou je peux rajouter des éléments bonus et décorer la scène avec des assets
* Dimanche : pareil basic puis tuned
* Lundi : pareil basic puis tuned    
* Mardi : je finis la version basic si c'est pas fini et je complète le readme
