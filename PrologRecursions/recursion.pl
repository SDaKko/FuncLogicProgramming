% Задание 1
%max(X, Y, Z) :- X > Y, Z is X.
%max(X, Y, X) :- X > Y.
max(X, Y, X) :- X > Y, !.
%max(X, Y, Y).
max(_, Y, Y). 

max(X, Y, Z, U) :- max(X, Y, V), max(V, Z, U).
max3(X, Y, Z, X) :- X > Y, X > Z, !.
max3(_, Y, Z, Y) :- Y > Z, !.
max3(_, _, Z, Z). 

%sum_cifr(0, 0). % Так нельзя, будет вызываться нижнее правило всегда - бесконечный цикл (как при true, так и при false)
sum_cifr(0, 0) :- !. %Рекурсия вверх. Отсечение, чтобы при успешном выполнении не проверялось следующее првило sum_cifr(N, S)
sum_cifr(N, S) :- Cifr is N mod 10, N1 is N div 10, sum_cifr(N1, S1),
S is S1 + Cifr.

%Рекурсия вниз
sum_cifr_down(N, S) :- sum_cifr_down(N, 0, S).

sum_cifr_down(0, Sum, Sum) :- !. %Дно рекурсии, третья sum унифицирована значением второй sum

sum_cifr_down(N, Cur_sum, Sum) :- Cifr is N mod 10, N1 is N div 10,
	New_cur_sum is Cur_sum + Cifr, sum_cifr_down(N1, New_cur_sum, Sum).

% Списки (Списки Черча)
write_list([]) :- !. % Дно рекурсии
write_list([H | T]) :- write(H), nl, write_list(T).

%Сумма элементов списка, рукурсия вниз

sum_el_down([H | T], S) :- sum_el_down([H | T], 0, S).
sum_el_down([], Sum, Sum) :- !.
sum_el_down([H | T], Cur_sum, Sum) :- New_cur_sum is Cur_sum + H, sum_el_down(T, New_cur_sum, Sum).


%Построить предикат, который удаляет из списка все элементы, сумма цифр которых равна данной.
remove_with_sum([], _, []).  % Базовый случай: пустой список

remove_with_sum([H|T], Sum, Result) :-
    sum_cifr_down(H, Sum_cifr),    % Если сумма цифр H равна Sum
    Sum_cifr = Sum,
    !,                        % Отсекаем выбор (не рассматриваем другие правила)
    remove_with_sum(T, Sum, Result).  % Пропускаем этот элемент

remove_with_sum([H|T], Sum, [H|Result]) :-  % Если сумма цифр не равна Sum
    remove_with_sum(T, Sum, Result).

% Задание 2 

% 1 Найти минимальную цифру числа.
%Рекурсия вверх
min_cifr(0, 9) :- !. %Базовый случай: когда число закончилось, возвращаем максимальную цифру
%min_cifr(Num, Min_cifr) :- Cifr is Num mod 10, Num1 is Num div 10, min_cifr(Num1, S1), (Cifr < S1 -> Min_cifr is Cifr ; Min_cifr is S1). %С условным выражением
min_cifr(Num, Cifr) :- Cifr is Num mod 10, Num1 is Num div 10, min_cifr(Num1, S1), Cifr < S1, !.  
min_cifr(Num, S1) :- Cifr is Num mod 10, Num1 is Num div 10, min_cifr(Num1, S1), Cifr >= S1. 

%Рекурсия вниз
% Минимальный элемент
min(A, B, A) :- A =< B, !.
min(A, B, B) :- A > B.

% Основной предикат
min_digit(Num, Min) :-
    min_digit_down(Num, 9, Min). % Начинаем с максимальной цифры 9

% Базовый случай - когда число обработано полностью
min_digit_down(0, CurrentMin, CurrentMin) :- !.

% Рекурсивный случай - обрабатываем следующую цифру
min_digit_down(Num, CurrentMin, Result) :-
    Digit is Num mod 10,
    min(Digit, CurrentMin, NewMin),
    NextNum is Num div 10,
    min_digit_down(NextNum, NewMin, Result).

% 2 Найти произведение цифр числа, не делящихся на 5

%Рекурсия вверх
mult_cifr(0, 1) :- !.
mult_cifr(Num, Mult) :- Cifr is Num mod 10, Num1 is Num div 10, Del is Cifr mod 5, Del \= 0, mult_cifr(Num1, M1), Mult is M1 * Cifr, !.
mult_cifr(Num, Mult) :- Num1 is Num div 10, mult_cifr(Num1, M1), Mult is 1 * M1.

%Рекурсия вниз
mult_digit(Num, Mult) :- mult_digit_down(Num, 1, Mult). %Аккумулятор посередине
mult_digit_down(0, CurrentMult, CurrentMult) :- !.
mult_digit_down(Num, CurrentMult, Result) :- Digit is Num mod 10, Del is Digit mod 5, Del \= 0,
NewMult is CurrentMult * Digit, NextNum is Num div 10, mult_digit_down(NextNum, NewMult, Result), !.
mult_digit_down(Num, CurrentMult, Result) :- NewMult is CurrentMult * 1, NextNum is Num div 10, mult_digit_down(NextNum, NewMult, Result).

% 3 Найти НОД двух чисел.

%Рекурсия вниз
% Базовый случай: НОД любого числа и 0 равен самому числу
gcd(X, 0, X) :- !.

% Основной рекурсивный случай (алгоритм Евклида)
gcd(X, Y, Result) :- Y > 0, Rest is X mod Y, gcd(Y, Rest, Result).
