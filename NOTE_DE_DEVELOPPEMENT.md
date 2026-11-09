# Note de développement

## Intention du projet

Ce dépôt a pour objectif de construire une application de gestion de tâches avec une interface Flutter en Dart et un service en C#. La première étape est la communication TCP entre les deux composants ; le modèle complet d’une tâche et les fonctionnalités finales seront précisés ensuite.

## Principes de travail

- Avancer par petites étapes et tester chaque partie avant d’ajouter la suivante.
- Garder le contrat JSON comme référence commune pour les noms des commandes et leurs paramètres.
- Générer les enums Dart et C# depuis ce contrat afin d’éviter les divergences entre les deux projets.
- Documenter les décisions de protocole, notamment le format des messages et la gestion des erreurs.
- Ne pas supposer qu’un service peut fonctionner en arrière-plan de la même façon sur toutes les plateformes : vérifier les contraintes de chaque cible.

## Apprendre en construisant

Les outils d’IA peuvent aider à comprendre une notion, comparer des approches ou analyser une erreur. Il est préférable de leur demander des explications et de vérifier les propositions plutôt que de reprendre du code sans le comprendre.

Avant d’intégrer une solution, s’assurer de pouvoir expliquer son fonctionnement, ses limites et la façon de la tester.
