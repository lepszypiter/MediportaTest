Treść zadania: Przygotować REST API w .NET 8 i C#, które wewnętrznie będzie oparte o listę tagów dostarczanych przez API StackOverflow (https://api.stackexchange.com/docs). Założenia projektu:

    pobrać min. 1000 tagów z API SO do lokalnej bazy danych lub innego trwałego cache
    pobrane może nastąpić na starcie lub przy pierwszym żądaniu, od razu w całości lub stopniowo tylko brakujących danych
    obliczyć procentowy udział tagów w całej pobranej populacji (źródłowe pole count, odpowiednio przeliczone)
    udostępnić tagi poprzez stronicowane API z opcją sortowania po nazwie i udziale w obu kierunkach
    udostępnić metodę API do wymuszenia ponownego pobrania tagów z SO
    udostępnić definicję OpenAPI przygotowanych metod API
    całość powinna się uruchamiać po wykonaniu wyłącznie polecenia "docker compose up"
