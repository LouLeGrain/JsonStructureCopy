# JsonStructureCopy
## Description
**JsonStructureCopy** est un petit utilitaire que j'ai codé pour garder à jour les fichiers de traduction JSON des applis Angular.<br/>
J'ai un fichier de traduction principal, mais j'ajoute et supprime des clés au fur et a mesure du développement.<br/>
A un moment il faut avoir les mêmes clés de traduction pour les autres langues.

## Utilisation
En ligne de commande : 
> `dotnet run -- source.json target.json`

Créé un fichier `result.json` qui donne le résultat du traitement

## Détail
On prend un fichier Json comme référence (appelons le `source.json`), puis on copie sa structure dans le deuxième fichier (`target.json`).

- Si le fichier source à une propriété que le fichier target n'a pas, on copie cette popriété et sa valeur dans le target.
- Si le fichier target à une propriété que le fichier source n'a pas, on supprime cette popriété et sa valeur dans le target.

On s'intéresse ici qu'aux propriétés contenant des types primaires (j'ai testé qu'avec des string cependant).<br/>
Donc si une propriété de la source contient elle même un objet Json, on appelle récursivement la méthode sur cet objet.<br/>

J'ai pas testé consciencieusement, j'ai juste regardé si le fichier de sortie avait le même nombre de lignes et si on retrouvait bien des clés qui n'y étaient pas qui y sont désormais.

## Exemple
### Un fichier de traduction en Français
```json
// source.json
{
  "login" : {
    "buttonLabel" : "Connexion",
    "pwdField" : "Mot de passe",
    "loginField" : "Login ou email"
  }
}
```
### Un fichier de traduction en Anglais
```json
// target.json
{
  "login" : {
    "buttonLabel" : "Login",
    "pwdField" : "Password",
    "willBeRemoved": "willBeRemoved"
  }
}
````

### Résultat
```json
// result.json
{
  "login" : {
    "buttonLabel" : "Login",
    "pwdField" : "Password",
    "loginField" : "Login ou email"
  }
}
```