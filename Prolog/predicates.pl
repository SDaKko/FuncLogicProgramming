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

% Задание 3. Вариант 3
% С предикатами
grand_so(X, Y) :- father(P, X), father(Y, P), man(X).
grand_sons(X) :- grand_so(Y, X), print(Y), nl, fail.

grand_ma_and_son(X, Y) :- (woman(X), grand_so(Y, X)) ; (woman(Y), grand_so(X, Y)).

uncle(X, Y) :- b_s(X, F), parent(F, Y), man(X).
uncles(X) :- uncle(Y, X), print(Y), nl, fail.

% С фактами

grand_so(X, Y) :- parent(P, X), parent(Y, P), man(X).
grand_sons(X) :- parent(X, P),  parent(P, Y), man(Y), print(Y), nl, fail.

grand_ma_and_son(X, Y) :- (woman(X), parent(P, Y), parent(X, P), man(X)) ; (woman(Y), parent(P, X), parent(Y, P), man(X)).

% Здесь 2 раза может быть true, так как сначала находит общего отца, затем общую мать, можно сделать отсечение
uncle(X, Y) :- parent(F, X), parent(F, P), parent(P, Y), man(X), !.
uncles(X) :- parent(F, X), parent(P, F), parent(P, D), man(P), D \= F, man(D), print(D), nl, fail.