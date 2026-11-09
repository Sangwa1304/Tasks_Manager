# Gestionnaire de tâches

Application de gestion de tâches composée d’une interface Flutter en Dart et d’un service en C#. L’objectif est de permettre à l’interface de communiquer avec le service pour créer, consulter, modifier et supprimer des tâches, ainsi que de gérer leurs notifications.

> **État du projet :** en développement. La communication TCP et le contrat entre les deux composants restent à mettre en place.

## Architecture prévue

- **Interface — Dart / Flutter**  
  Affiche les tâches et permet à l’utilisateur de les gérer. Au démarrage, elle se connectera au service pour récupérer les tâches et mettre son affichage à jour.

- **Service — C#**  
  Reçoit les commandes de gestion des tâches via TCP et prend en charge les opérations liées aux tâches et aux notifications.

- **Contrat partagé — JSON**  
  Décrit les commandes disponibles, leurs paramètres et le type de données retourné. Un script Python doit permettre de générer les enums correspondants pour Dart et C#.

## Contrat de communication envisagé

Les commandes seront échangées au format JSON. Le contrat proposé comprend les opérations suivantes :

| Commande | Paramètres | Résultat |
|---|---|---|
| `newTache` | `titre`, `heure` | `Tache` |
| `supprimerTache` | `id` | `bool` |
| `modifierTache` | `id`, `nouveauTitre` | `Tache` |
| `getTache` | `id` | `Tache` |
| `getToutesLesTaches` | aucun | `List<Tache>` |
| `createNotificationForTache` | `id`, `heure` | `bool` |

Le fichier de contrat et le script de génération seront ajoutés au dépôt. Le modèle `Tache` côté Flutter devra ensuite permettre de désérialiser les réponses du service.

## Points à préciser

Avant de considérer le protocole comme stable, il faudra notamment définir :

- la structure exacte des messages JSON envoyés et reçus ;
- la manière de délimiter les messages sur la connexion TCP ;
- le traitement des erreurs et des réponses invalides ;
- le comportement du service en arrière-plan selon les plateformes ciblées ;
- le modèle définitif d’une tâche et le format des dates et heures.

## Technologies

- [Flutter](https://flutter.dev/) / Dart
- C#
- Python pour la génération des enums à partir du contrat JSON

## Développement

Le projet évoluera progressivement : commencer par établir et tester la communication TCP, puis ajouter le contrat partagé, la génération des enums et l’intégration du modèle `Tache` dans l’interface.
