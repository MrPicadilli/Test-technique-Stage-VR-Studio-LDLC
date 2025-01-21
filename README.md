# Test-technique-Stage-VR-Studio-LDLC
Le test technique que demande LDLC VR studio pour tous les candidats au stage pour le poste de "Développeur.euse Unity Jeu Vidéo en VR"

Date limite d'envoi : **mardi 21 janvier 23h59** (un retour anticipé est bien sûr possible).

Commandes :
* zqsd = se déplacer
* clic gauche = attraper un objet
* clic droit = relacher un objet


# Information supplémentaire 
## Etapes de développement
A chaque fois je procédais de la meme facon :
* créer tous les visuels qui seront utilses pour la fonctionnalité en question
* faire le script en question
* tester/debug
* nettoyer le code + commentaire ou attribut

Dans l'ordre de développement j'ai procédé comme comme ci : 
1. Character Controller
    *  Vue FPS
    *  Mouvement
2. interagir avec les objets
    * grab + feedback
    * throw + feedback
3. Elément de gameplay
    * temps imparti
    * points
4. UI de fin
    * écran de victoire
    * écran de défaite
    * possibilité de recommencer le niveau
5. faire un sciptable object pour les paramètres de jeu
6. faire un custom editor pour le sciptable object
7. faire les ajouts bonus
## Temps passé
* jeudi : 1h
* vendredi : 3h
* samedi : 0h
* dimanche : 4h
* lundi : 3h
* mardi : 6h

total = 1+3+0+4+3+6 = 17 h
## choix importants
* Problèmes : Dois je appliquer de la gravité sur le joueur ?
    *  dans la version basic oui pourquoi pas mais au début du développement je devais juste faire le stricte minimum donc j'ai repoussé ce choix
## difficultés rencontrées
* **titre : changement de parent pour attraper un objet**
    * **Contexte** : Lorsque je devais implémenter le fait de pouvoir attraper un objet je pouvais soit faire la technique que je fais actuellement pour la camera qui est de faire suivre dans un script l'objet à la main sauf que comme je l'avais déja fait pour la caméra j'ai trouvé dommage de faire la même méthode du coup j'ai essayé de parenté l'objet attrapé au player 

    * **Problème** : je voulais mettre l'objet dans la main droite en utilisant un empty object et comme je n'ai pas de modele propre j'utilise donc des formes primitives ou je modifie le scaling/rotation. Sauf que lorsque j'essai de parenté un objet a un autre l'enfant essai de s'adapter par rapport a son parent en faisant des conversion global->local ce qui amène a avoir mon enfant qui est modifié en terme de scaling et de rotation et qui peut ne plus du tout ressembler à l'objet de départ.
    * **Solution** : je l'ai mis dans un emplacement ou il n'y a aucune modification de rotation ou de scale dans tout les parent de l'empty object qui prend la position de l'objet attrapé. Comme il n'y a plus de conversion a faire plus de problème.
* **titre : le mouvement du joueur ne suit pas l'orientation de la caméra**
    * **Contexte** : pour faire le character controller il fallait que je respecte le principe du fps et que lorsque je veux aller de l'avant je dois avancer dans la direction de la caméra du joueur et non dans le repère global.
    * **Problème** : n'ayant pas pris en compte au départ du probleme de l'orientation dynamique du joueur le joueur avancait par rapport au repère globale ce qui faisait que je pouvais presser z et aller en arrière.
    * **Solution** : j'ai rajouté une notion d'orientation que j'utilise pour avoir des vecteurs local au joueur afin de me diriger.
    
## Passage en VR
### En terme technique
#### Mouvements du joueur
* rajouter une version prefete de module XR (XR origin) et modifier les parametres pour pouvoir se déplacer en se téléportant
#### UI
* bien penser a mettre les "render mode" des canvas en "world space" et ajuster les tailles et ajouter le component "tracked device graphic raycaster"  au canva et le component "XR ui input module" à l'event system si jamais on veut pouvoir interagir avec l'UI     
#### Mécaniques d'attraper un objet
* enlever toutes les mécaniques de clic gauche / clic droit puis mettre un component "XR Grab Interactable" sur tout les interactible
* Feedback : utiliser l'affordance system qui est deja dans le kit d'openXR qui remplacera ce que j'ai pu faire en terme de feedback pour attraper un objet mais je ne pense pas qu'il remplacera le code pour déposer l'objet dans le container.


### En terme d'expérience

#### Mouvements du joueur
* **Question 1** : Comment déplace t-on l'utilisateur ?
* **Réponse perso** : plusieurs options :
    * **déplacement physique** : me parait être la meilleur idée en terme de réalisme et de confort pour éviter la cybersickness apres le probleme c'est qu'il faut adapter le terrain 3D au terrain physique don souvent faut le faire dans un gymnase
    * par **téléportation libre** : me parait etre la meilleur idée si on a pas  un gymnase il y aura juste un problème au niveau de la parti chronometré car en l'etat actuel le jeu pourra se finir trop rapidement suivant la maitrise du joueur de la VR mais en mettant des couloir et une notion de jeu de plateforme on peut augmenter le fun et la difficulté
    * par **téléportation fixe** : (tp avec des points d'ancrage ) mauvaise idée et inutile dans ce contexte si on peut faire de la tp libre
    * on bouge **avec les joystick** : surement la pire solution que j'ai en tête  car c'est le meilleur moyen pour se taper un mal de crane en moins de 10 min.
#### UI

Je vois 2 questions a se poser :
* **Question 1** : doit on faire suivre l'UI directement sur la caméra du joueur comme c'est le cas actuellement ?
* **Réponse perso** : Non ca n'a jamais été une bonne idées de faire ca en VR et cela peut devenir gênant pour le joueur en terme de cybersickness. Donc il vaut mieux mettre l'UI en orbite qqpart dans le niveau de facon statique ou sinon sur lesmains de l'avatar qui se fait aussi.

* **Question 2** : comment gere t-on l'ui de fin ?
* **Réponse perso** : dans tout lescas il ne faut pas freeze le personnage à la fin et il faudrait juste faire apparaitre une fenetre statique qui montre l'actuel ecran de fin afin que le joueur utilise un rayon pour pouvoir sélectioner l'option play again.

#### Mécaniques d'attraper un objet

Je vois 2 questions a se poser :
* **Question 1** : peut on attraper un objet a distance avec un systeme de ray ?
* **Réponse perso** : cela dépend de ce qu'on priviligie soit on part sur qqchose de réaliste donc l'utilisateur doit se baisser pour attraper un objet qui est tombé ou autre soit on utilise le rayon ca diminue le réalisme donc l'immersion mais est plus confortable et pratique une fois maitrisé.

* **Question 2** : une fois qu'un interactible est attrapé bloque t-on le joueur de le déposer n'importe ou sauf dans la zone de point.
* **Réponse perso** : on ne devrait pas bloquer le joueur de déposer l'interactible ou il en a envie sinon ce serait trop frustrant

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


Rien n'a été fait parmi cette liste d'idées mais si jamais je me retrouve a retravailler sur ce projet je pense avancer le gameplay en priorité puis essayer de faire une version unity.

# Feuille de route
* Jeudi : mise en place du git et je commence a planifier / réfléchir à l'implémentation
* Vendredi : je commence le code pour faire le gameplay basic qui est demandé en en faisant le stricte minimum.
* Samedi : selon comment j'ai avancé le gameplay basic soit je le continu soit à partir de la scene basique je crée la scene "tuned gameplay" ou je peux rajouter des éléments bonus et décorer la scène avec des assets
* Dimanche : pareil basic puis tuned
* Lundi : pareil basic puis tuned    
* Mardi : je finis la version basic si c'est pas fini et je complète le readme
