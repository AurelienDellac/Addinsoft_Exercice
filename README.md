# Exercice Addinsoft

Ce projet a été réalisé dans le cadre d'un exercice soumit par Addinsoft. Le projet est une solution C# .NET contenant deux projets :
- Un client WPF .NET Core
- Un serveur API REST ASP .NET Core

# Installation

Pour installer le projet, il faut cloner la branche master du repository GitHub ou en télécharger l'archive zip depuis cette branche.

Avant de pouvoir utiliser le projet, il faut configurer les clés d'API et les tokens utilisés par le serveur. Ces données doivent être stockées dans les user-secrets .NET. il faut donc se placer dans le répertoire du projet Server et utiliser la commande suivante pour activer les user-secrets :

`dotnet user-secrets init`

Il faut ensuite ajouter les données nécessaires au Server avec les commandes suivantes :

`dotnet user-secrets set "Addinsoft:XlstatApiToken" "3615montoken"`

`dotnet user-secrets set "Addinsoft:CurrencyConvertApiKey" "21c08906a273c47db105"`

Ces deux commandes ajoutent respectivement le token pour l'API Xlstat et la clé pour l'API de conversion de devises.
L'installation est terminée il ne reste plus qu'à lancer les deux programmes.

