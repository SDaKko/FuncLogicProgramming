% Английские клубы
club(manchester_united, 1, 2, 1, 4, 3). % 1878
club(liverpool, 1, 1, 1, 4, 3).        % 1892
club(arsenal, 1, 1, 1, 2, 3).          % 1886
club(chelsea, 1, 1, 1, 3, 3).          % 1905
club(manchester_city, 1, 1, 1, 1, 3).  % 1880
club(tottenham, 1, 1, 1, 1, 2).        % 1882
club(leicester, 1, 1, 1, 1, 2).        % 1884
club(everton, 1, 1, 1, 1, 2).          % 1878

% Испанские клубы
club(real_madrid, 2, 1, 1, 4, 3).      % 1902
club(barcelona, 2, 1, 1, 4, 3).        % 1899
club(atletico_madrid, 2, 1, 1, 2, 3).  % 1903
club(valencia, 2, 1, 1, 2, 2).         % 1919

% Немецкие клубы
club(bayern_munich, 3, 1, 1, 4, 3).    % 1900
club(borussia_dortmund, 3, 2, 1, 2, 3).% 1909
club(schalke, 3, 1, 2, 1, 2).          % 1904

% Итальянские клубы
club(juventus, 4, 1, 1, 3, 2).         % 1897
club(milan, 4, 1, 1, 3, 2).            % 1899
club(inter, 4, 1, 1, 3, 2).            % 1908
club(roma, 4, 1, 1, 1, 2).             % 1927

% Французские клубы
club(psg, 5, 3, 1, 1, 2).              % 1970
club(marseille, 5, 1, 1, 1, 2).        % 1899
club(lyon, 5, 2, 1, 1, 2).             % 1950

% Другие страны
club(ajax, 6, 1, 1, 3, 2).             % Нидерланды, 1900
club(porto, 6, 1, 1, 2, 2).            % Португалия, 1893
club(benfica, 6, 1, 1, 2, 3).          % Португалия, 1904
club(celtic, 6, 1, 1, 1, 2).           % Шотландия, 1887
club(zenit, 6, 2, 1, 1, 2).            % Россия, 1925
club(krasnodar, 6, 4, 1, 1, 2).            % Россия, 2008

question1(Country) :- write('Club country: '), nl,
    write('1: England, 2: Spain, 3: Germany, 4: Italy, 5: France, 6: Other'), nl,
    read(Country).

question2(Era) :- write('Year of foundation: '), nl,
    write('1: Before 1900, 2: 1900-1949, 3: 1950-1999, 4: After 2000'), nl,
    read(Era).

question3(League) :- write('League level: '), nl,
    write('1: First division, 2: Second division, 3: Third and below'), nl,
    read(League).

question4(Titles) :- write('International titles: '), nl,
    write('1: No, 2: 1-5, 3: 6-10, 4: 10+'), nl,
    read(Titles).

question5(Stadium) :- write('Stadium capacity: '), nl,
    write('1: <30k, 2: 30-60k, 3: >60k'), nl,
    read(Stadium).

identify_club :- question1(Country), question2(Era),
    question3(League), question4(Titles),
    question5(Stadium),
    club(Name, Country, Era, League, Titles, Stadium),
    write('Possible club: '), write(Name), nl,
    fail.