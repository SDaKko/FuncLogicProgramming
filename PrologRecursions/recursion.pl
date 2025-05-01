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

% Задание 3
% Дан целочисленный массив и натуральный индекс (число, меньшее размера массива). Необходимо определить является ли элемент по указанному индексу глобальным максимумом.

% Предикат чтения 
% Используется для подготовки данных и запуска обработки
check_global_max(List, Index, Result) :-
    % Проверяем корректность индекса
    length(List, Len),
    Index < Len,
    % Получаем элемент по индексу
    get_element(List, Index, Element),
    % Проверяем, является ли он глобальным максимумом
    is_global_max(List, Element, Result).

% Предикат логики работы (основная логика)
% Проверяет, является ли элемент глобальным максимумом в списке
is_global_max([], _, true).
is_global_max([H|_], Element, false) :-
    H > Element,
    !.
is_global_max([_|T], Element, Result) :-
    is_global_max(T, Element, Result).

% Предикат для получения элемента по индексу (вспомогательный)
% Реализация через списки Черча
get_element(List, Index, Element) :-
    nth0(Index, List, Element). % Возвращает Element списка по заданному индексу

% Предикат вывода

print_result(List, Index) :-
    check_global_max(List, Index, Result),
    (
        Result == true,
        write('Element with index '),
        write(Index),
        write(' is global max'),
        nl
    ;
        Result == false,
        write('Element with index '),
        write(Index),
        write(' IS NOT global max'),
        nl
    ), !.

% Дан целочисленный массив и натуральный индекс (число, меньшее размера массива). Необходимо определить является ли элемент по указанному индексу локальным минимумом.

% Главный предикат
is_local_min(List, Index, Result) :-
    % Проверка корректности индекса
    length(List, Len),
    Index < Len,
    Index >= 0,
    % Получение элемента и его соседей
    get_element(List, Index, Element), % Реализован для предыдущей задачи
    get_neighbors(List, Index, Left, Right),
    % Проверка условия локального минимума
    check_local_min(Element, Left, Right, Result).

% Предикат логики: проверка условия локального минимума
check_local_min(Element, no_neighbor, Right, Result) :-
    (Right >= Element -> Result = true ; Result = false), !.
check_local_min(Element, Left, no_neighbor, Result) :-
    (Left >= Element -> Result = true ; Result = false), !.
check_local_min(Element, Left, Right, Result) :-
    (Left >= Element, Right >= Element -> Result = true ; Result = false).

get_neighbors(List, Index, Left, Right) :-
    length(List, Length),
    (Index =:= 0 -> Left = no_neighbor ; Prev is Index-1, nth0(Prev, List, Left)),
    (Index =:= Length-1 -> Right = no_neighbor ; Next is Index+1, nth0(Next, List, Right)).

% Предикат вывода
print_local_min_result(List, Index) :-
    is_local_min(List, Index, Result),
    (
        Result == true,
        write('Element with index '),
        write(Index),
        write(' is local min'),
        nl
    ;
        Result == false,
        write('Element with index '),
        write(Index),
        write(' IS NOT local min'),
        nl
    ), !.

% Дан целочисленный массив. Необходимо осуществить циклический сдвиг элементов массива влево на одну позицию.
% Главный предикат
cyclic_shift_left(List, ShiftedList) :-
    % Проверка, что список не пустой
    List \= [],
    % Выполнение сдвига
    perform_shift_left(List, ShiftedList).

% Предикат логики: выполнение циклического сдвига
perform_shift_left([Head|Tail], ShiftedList) :-
    append(Tail, [Head], ShiftedList).  % Соединяем хвост с головой

% Предикат вывода
print_shifted_list(List) :-
    (cyclic_shift_left(List, ShiftedList) ->
        write('Original list: '), write(List), nl,
        write('Shifted list: '), write(ShiftedList), nl
    ;
        write('Error: empty list'), nl), !.

% Задание 5

% Найти максимальный простой делитель числа.

% Главный предикат (интерфейс)
max_prime_factor(N, Result) :-
    N > 1,                     % Проверка ввода
    find_prime_factors(N, 2, [], Factors),
    max_member(Result, Factors). % Встроенный предикат для поиска максимума

show_max_prime_factor(N) :-
    (max_prime_factor(N, Result) ->
        write('Max simple divider of number is '),
        write(N), write(': '), write(Result), nl
    ;
        write('Error: the number must be bigger then 1'), nl
    ).

% Рекурсивный поиск простых делителей
find_prime_factors(N, D, Acc, Factors) :-
    (D > N -> 
        Factors = Acc
    ;
        (N mod D =:= 0, prime(D) ->
            NewAcc = [D|Acc],
            NextD is D + 1
        ;
            NewAcc = Acc,
            NextD is D + 1
        ),
        find_prime_factors(N, NextD, NewAcc, Factors)
    ).

% Проверка на простоту
prime(2).
prime(N) :-
    N > 2,
    not(has_divisor(N, 2)).

% Проверка наличия делителей
has_divisor(N, D) :-
    D * D =< N, %Проверка делителей только до корня из N
    (N mod D =:= 0 ; Next is D + 1, has_divisor(N, Next), !).

%Найти НОД максимального нечетного непростого делителя числа и произведения цифр данного числа.

%Рекурсия вниз, произведение цифр числа
mult_digit_base(Num, Mult) :- mult_digit_down_base(Num, 1, Mult). %Аккумулятор посередине
mult_digit_down_base(0, CurrentMult, CurrentMult) :- !.
mult_digit_down_base(Num, CurrentMult, Result) :- Cifr is Num mod 10, N1 is Num div 10, New_cur_mult is CurrentMult * Cifr, mult_digit_down_base(N1, New_cur_mult, Result).

% Главный предикат
main_task(N, Result) :-
    N > 1,
    find_max_odd_composite_divisor(N, MaxDiv), % Поиск максимального нечетного непростого делителя
    mult_digit_base(N, Product), % Произведение цифр числа
    gcd(MaxDiv, Product, Result). %НОД максимального нечетного непростого делителя числа и произведения цифр данного числа, gcd реализован сверху в коде

% Поиск максимального нечетного непростого делителя
find_max_odd_composite_divisor(N, MaxDiv) :-
    find_divisors(N, 1, [], Divisors),
    filter_odd_composite(Divisors, Filtered),
    max_member(MaxDiv, Filtered). % Встроенный предикат для поиска максимума в списке

% Поиск всех делителей числа (рекурсивно)
find_divisors(N, D, Acc, Divisors) :-
    (D > N -> 
        Divisors = Acc
    ;
        (N mod D =:= 0 ->
            NewAcc = [D|Acc],
            NextD is D + 1
        ;
            NewAcc = Acc,
            NextD is D + 1
        ),
        find_divisors(N, NextD, NewAcc, Divisors)
    ).

% Фильтрация нечетных непростых делителей
filter_odd_composite([], []).
filter_odd_composite([H|T], [H|Rest]) :-
    H mod 2 =:= 1,
    not(prime(H)),
    filter_odd_composite(T, Rest).
filter_odd_composite([H|T], Rest) :-
    (H mod 2 =:= 0 ; prime(H)),
    filter_odd_composite(T, Rest).


% Предикат вывода
show_result(N) :-
    (main_task(N, Result) ->
        write('GCD of max odd composite divisor and digit product of '),
        write(N), write(' is '), write(Result), nl
    ;
        write('Error: number must be greater than 1'), nl
    ).