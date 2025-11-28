# NaniFutures (Naninovel Futures)
This is an (unofficial) code repository that exists specifically to 
propose new features and attempt to incorporate these features 
into the main branch of Naninovel (a Unity visual novel engine).

We hope that by importing this package, participants in discussions 
about the proposed new features of the engine will be able to easily 
and intuitively explore the impact of these new features on their 
coding process, thereby providing a basis for making a final decision.

## Installation
In Unity versions 6000.0 - 6000.3, you can install this package by 
selecting "Install package from git URL..." through the package manager.

## Implementation

To avoid the maintenance difficulties caused by direct modifications 
to the Naninovel engine source code itself, all new features 
implemented here have the extension forms recommended by Naninovel.

However, our initial implementation of these features was typically 
done by directly applying these modifications to the engine source 
code. In the process of transitioning from this implementation to 
the feature implementation method used in this project, some 
information may be lost because we cannot find a way to implement 
them without modifying the engine source code (e.g., adding comments 
to ICharacterManager).

## Compatibility and consistency

Given the nature of this project, this package will only be developed 
for the latest code branch of Naninovel. Once new features implemented 
in this project are accepted and merged into Naninovel's main branch, 
their implementations will be removed from this repository.

However, if these features are ultimately not approved, they may still 
be retained as extensions—in other words, while ensuring consistency 
with the coding experience of the engine's main branch is our ongoing 
goal, features that we believe are not yet, and seem unlikely to be 
added to the main branch in the future, still have their necessity 
and will continue to exist.

## License
Any new content added to this project is licensed under the MIT License.