# JsonStructureCopy
**JsonStructureCopy** is a small utility to keep JSON translation files for Angular apps up to date. <br/>
I have a main translation file, but I add and remove keys as I develop.<br/>
At some point, I need to have the same translation keys for the other languages files.

## Usage
In the command line:
> `dotnet run -- source.json target.json`

Creates a `result.json` file that contains the processing result.

You can also run it without arguments, the program will then ask the paths for the two files.

## Details
We take a Json file as a reference (let's call it `source.json`), and then we copy its structure into the second file (`target.json`).

- If the source file has a property that the target file doesn't have, we copy that property and its value into the target file.
- If the target file has a property that the source file doesn't have, we remove that property and its value from the target file.

Here, we are only interested in properties containing primitive types (I tested only with strings, though).
So, if a property in the source file itself contains a Json object, we recursively call the method on that object.

I haven't thoroughly tested it, but the results seems good so far.

## Example
### A French translation file
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
### An English translation file
```json
// target.json
{
  "login" : {
    "buttonLabel" : "Login",
    "pwdField" : "Password",
    "willBeRemoved": "willBeRemoved"
  }
}
```

### Result
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
These examples are included in the `test` folder to try it out

---
<span style="font-size:2rem">🇫🇷</span>
# JsonStructureCopy
**JsonStructureCopy** est un petit utilitaire pour garder à jour les fichiers de traduction JSON des applis Angular.<br/>
J'ai un fichier de traduction principal, mais j'ajoute et supprime des clés au fur et a mesure du développement.<br/>
A un moment il faut avoir les mêmes clés de traduction pour les autres langues.

## Utilisation
En ligne de commande : 
> `dotnet run -- source.json target.json`

Créé un fichier `result.json` qui donne le résultat du traitement.

Il est aussi possible de lancer le programme sans arguments, les deux chemins vers les fichiers json vous seront demandés.

## Détail
On prend un fichier Json comme référence (appelons le `source.json`), puis on copie sa structure dans le deuxième fichier (`target.json`).

- Si le fichier source à une propriété que le fichier target n'a pas, on copie cette popriété et sa valeur dans le target.
- Si le fichier target à une propriété que le fichier source n'a pas, on supprime cette popriété et sa valeur dans le target.

On s'intéresse ici qu'aux propriétés contenant des types primaires (j'ai testé qu'avec des string cependant).<br/>
Donc si une propriété de la source contient elle même un objet Json, on appelle récursivement la méthode sur cet objet.<br/>

J'ai pas testé consciencieusement, mais les résultats sont probants jusqu'ici.

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

Ces exemples sont inclus dans le dossier `test`