# MINI-Game-Of-Life
A recreation of John Conway's 'Game of Life' in Unity.
The gif shows a **154x86** grid of cellular automata working with the rules of 'Game of Life'.

To make it visually more interesting I added that the cell's color strength/opacity ( its alpha ) is determined by the amount of generations it survived.
If the cell is alive for one generation, it's alpha has a value of `0.1f`.
If it survived for ten generations ( or longer ), it's color strength is max, meaning its alpha is `1.0f`


## Wiki/Reference: 
https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life

https://en.wikipedia.org/wiki/Cellular_automaton

<img align="both">
<br clear="both"/>

![Game Of Life](https://user-images.githubusercontent.com/47507160/122291291-f3a5bf00-cef4-11eb-9eaf-81bf0e782332.gif)
