% Задание 1
man :- man(X), print(X), nl, fail.
woman :- woman(X), print(X), nl, fail.
children(X) :- parent(X, Y), print(Y), nl, fail.
mother(X, Y) :- parent(X, Y), woman(X).
father(X, Y) :- parent(X, Y), man(X).
mother(X) :- mother(Y, X), print(Y).
father(X) :- father(Y, X), print(Y).
brother(X, Y) :- X\=Y, man(X), father(F, X), father(F, Y), mother(M, X), mother(M, Y).
brothers(X) :- father(F, X), mother(M, X), father(F, Brother), 
mother(M, Brother), man(Brother), print(Brother), nl, fail.
b_s(X, Y) :- father(F, X), father(F, Y), mother(M, X), mother(M, Y), X \= Y.
b_s(X) :- b_s(X, Y), print(Y), nl, fail.

% Задание 2. Вариант 3
daughter(X, Y) :- woman(X), parent(Y, X), !.
daughter(X) :- daughter(Y, X), print(Y).
wife(X, Y) :- woman(X), man(Y), parent(X, C), parent(Y, C), !.
wife(X) :- wife(Y, X), print(Y).
