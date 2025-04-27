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

%sum_cifr(0, 0).
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
