# Exercice Addinsoft

Ce projet a été réalisé dans le cadre d'un exercice soumit par Addinsoft. Le projet est une solution C# .NET contenant deux projets :
- Un client WPF .NET Core
- Un serveur API REST ASP .NET Core

# Installation

Pour installer le projet, il faut cloner la branche ***develop*** du repository GitHub ou en télécharger l'archive zip depuis cette branche. Ensuite, il faut ouvrir la solution (AddinsoftExercice.sln) dans Visual Studio.

Avant de pouvoir utiliser le projet, il faut configurer les clés d'API et les tokens utilisés par le serveur. Ces données doivent être stockées dans les user-secrets .NET. Il faut donc se placer dans le ***répertoire du projet Server*** et utiliser la commande suivante pour activer les user-secrets (cf. plus loin pour plutôt utiliser le fichier appsettings.json) :

`dotnet user-secrets init`

Il faut ensuite ajouter les données nécessaires au Server avec les commandes suivantes :

`dotnet user-secrets set "Addinsoft:XlstatApiToken" "3615montoken"`

`dotnet user-secrets set "Addinsoft:CurrencyConvertApiKey" "21c08906a273c47db105"`

Il est aussi possible d'indiquer ces champs dans le fichier ***appsettings.json*** du serveur. Cela aura pour avantage de fonctionner avec un build du projet. Il faut donc ajouter ces deux lignes au fichier :

`"Addinsoft:XlstatApiToken": "3615montoken"`

`"Addinsoft:CurrencyConvertApiKey": "21c08906a273c47db105"`

Ces deux commandes ajoutent respectivement le token pour l'API Xlstat et la clé pour l'API de conversion de devises.
L'installation est terminée, il ne reste plus qu'à lancer les deux programmes, soit depuis Visual Studio, soit en faisant un build et donc avec leur exécutable respectif. 

Dans le cas de l'utilisation de builds, il faudra changer l'adresse utilisée par le client. Cette modification se fait dans le fichier Client.dll.config du build. Il faut changer le port pour celui indiqué par le serveur dans la console qui s'ouvre en le lançant depuis le build (5001 normalement).

# Choix techniques
Les deux programmes sont réalisés en C# .NET Core car c'est une technologie que j'apprécie et que je n'avais j'amais réalisé d'API ainsi, c'était donc l'occasion de m'y essayer.

# Problématiques rencontrées
Aucune réelle problématique ne s'est imposée à moi exceptée celle de faire partager un repository à plusieurs projets C#. Au final j'ai opté pour mettre les deux projets dans une seule Solution. Je ne sais pas si c'est correct car j'avoue ne pas totalement maitriser le concept de Solution C# ne l'ayant que peu exploité en tant que tel.

Aussi, dans le cadre de développement d'API j'ai toujours un doute sur où gérer les erreurs : en amont sur le client ou plutôt au niveau de l'API ? Surement les deux mais je suis toujours hésitant donc cette aspect n'est pas le mieux conçu.

# Choix de conception
Malgré la taille très réduite des projets j'ai essayé de respecter au mieux les bonnes pratiques de conception objet. J'ai donc mis en place un pattern Repository et de l'injection de dépendances. Les deux projets ont une architecture MVC/MVVM.

# Améliorations possibles
Plusieurs améliorations peuvent être apportées à la solution :
- les informations utilisées par l'API comme les clés ou tokens pourraient être cryptées ou au moins encodées ;
- le client pourrait utiliser un token pour se connecter au serveur API ;
- les erreurs remontées par les différentes API distantes pourraient être mieux remontées jusqu'au client.

