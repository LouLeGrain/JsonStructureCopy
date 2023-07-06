using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace JsonStructureCopy
{
  class Program
  {
    public static void Main(string[] args)
    {
      // On récupère les deux fichiers JSON
      string source = string.Empty;
      using (StreamReader reader = new($"{args[0]}"))
      {
        source = reader.ReadToEnd();
      }

      var target = string.Empty;
      using (var reader = new StreamReader($"{args[1]}"))
      {
        target = reader.ReadToEnd();
      }

      // On les parse
      JObject jsonSource = JObject.Parse(source);
      JObject jsonTarget = JObject.Parse(target);

      MergeJsonProperties(jsonSource, jsonTarget);

      using (var writer = new StreamWriter(@"result.json"))
      {
        writer.Write(jsonTarget.ToString());
      }
    }

    /// <summary>
    /// Méthode récursive, qui merge les valeurs de la source dans le target :
    /// - si une propriété est dans la source mais pas dans le target, elle est copiée
    /// - si une propriété est dans le target mais pas dans la source, elle est supprimée
    /// </summary>
    /// <param name="source">Objet json dont on veut copier la structure</param>
    /// <param name="target">Object json dans lequel on va copier la structure</param>
    public static void MergeJsonProperties(JObject source, JObject target)
    {
      // on convertit les propriétés des JSON en listes sur lesquelles on peut itérer
      List<JProperty> sourceProperties = source.Properties().ToList();
      List<JProperty> targetProperties = target.Properties().ToList();

      // Ajout des propriétés de la source qui ne sont pas dans le target
      foreach (JProperty sourceProp in sourceProperties)
      {
        // on cherche dans le target si la propriété existe
        JProperty? targetProp = targetProperties.FirstOrDefault(p => p.Name == sourceProp.Name);

        // Si on l'a pas trouvée, on l'ajoute
        if (targetProp == null)
        {
          target.Add(sourceProp.Name, sourceProp.Value);
        }
        // Sinon si on l'a trouvée et que c'est un objet avec d'autres propriétés, on appelle cette même méthode sur cet objet
        else if (sourceProp.Value.Type == JTokenType.Object && targetProp.Value.Type == JTokenType.Object)
        {
          MergeJsonProperties((JObject)sourceProp.Value, (JObject)targetProp.Value);
        }
        // Si on arrive ici, on a trouvé une propriété dans le target qui est la même que dans la source, 
        // et qui n'est pas un objet avec d'autres propriétés, c'est donc une valeur que l'on veut garder telle quelle, donc on fait rien
      }

      // Retrait des propriétés qui sont dans le target mais pas dans la source
      foreach (var targetProp in targetProperties)
      {
        if (!sourceProperties.Any(p => p.Name == targetProp.Name))
        {
          targetProp.Remove();
        }
      }
    }
  }
}
