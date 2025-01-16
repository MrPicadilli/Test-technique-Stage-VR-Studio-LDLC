# Test-technique-Stage-VR-Studio-LDLC
Le test technique que demande LDLC VR studio pour tous les candidats au stage pour le poste de "Développeur.euse Unity Jeu Vidéo en VR"

Date limite d'envoi : **mardi 21 janvier 23h59** (un retour anticipé est bien sûr possible).
# Information supplémentaire 
## Etapes de développement
## Temps passé
* jeudi : 1h
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
- [ ] Character Controller
    - [ ] déplacements avant / arrière – gauche / droite
    - [ ] contrôle de la vue à la souris
- [ ] interagir avec les objets
    - [ ] Clic gauche = prendre objet (le faire apparaitre dans une des 2 mains)
    - [ ] Clic droit = lacher objet
    - [ ] ajouter les feedback pour prendre l'objet ou lorsque l'on est a une certaine distance + la souris pointe sur l'objet alors feedback **jaune**
    - [ ] ajouter les feedback pour lacher l'objet ou lorsque l'on dans la zone du conteneur avec un objet cette objet qui est dans les main apparait en **bleu**
- [ ] Elément de gameplay
    - [ ] temps imparti
        - [ ] mettre un chrono en haut au milieu de la vue du joueur
        - [ ] mettre un compteur d'objet sur la vue de l'utilisateur
    - [ ] ecran de fin
        - [ ] game Over (affichant le nombre d’éléments restants qui n’ont pas été déplacés à temps)
        - [ ] Bravo !
- [ ] faire un scriptable object pour pouvoir modifier : 
    - [ ] Temps de jeu
    - [ ] Nombre de pièces à déplacer pour gagner la partie 
- [ ]  configuration avec un Custom Editor qui présente de manière agréable l’édition du 
scriptableobject, à destination de l’équipe design.

## Tuned Gameplay


- Shaders 
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
    - faire 3 niveau oul'on augmente la difficulté à chaque fois
        - easy = le conteneur ne bouge pas
        - medium le conteneur se téléporte a chaque lancé réussi
        - hard le conteneur bouge avec un certain pattern 
## VR Gameplay

# Feuille de route
* Jeudi : mise en place du git et je commence a planifier / réfléchir à l'implémentation
* Vendredi : je commence le code pour faire le gameplay basic qui est demandé et en faire le stricte minimum de ce qui est demandé pour faire tous ce qui est demandé sauf les bonus (audio, shader...)
* Samedi : selon comment j'ai avancé le gameplay basic soit je le finis soit à partir de la scene basique je crée la scene "tuned gameplay" ou je peux rajouter des éléments bonus et décorer la scène avec des assets
* Dimanche : je finis la version tuné
* Lundi : Une fois que j'ai fini d'écrire le readme et que je partage le comment je ferais pour passer l'appli en mode VR je peux commencer une version VR à partir de la version tuné
* Mardi : je vois pour finir la version VR et essaie d'envoyer le test avant 20h00 max.
