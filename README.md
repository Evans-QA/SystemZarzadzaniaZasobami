# System zarządzania zasobami firmy

Projekt przedstawia prostą aplikację rozproszoną napisaną w języku C#.

Aplikacja składa się z dwóch części:

- ZasobyApi - serwer API,
- SystemZarzadzaniaZasobami - klient konsolowy.

## Cel projektu

Celem projektu jest stworzenie prostej aplikacji klient-serwer, która symuluje system zarządzania zasobami w firmie.

## Funkcje aplikacji

Aplikacja umożliwia:

- wyświetlanie listy zasobów,
- dodawanie nowego zasobu,
- rezerwowanie zasobu,
- zwalnianie zasobu,
- usuwanie zasobu.

## Architektura aplikacji

Projekt działa w architekturze klient-serwer.

Serwer `ZasobyApi` udostępnia API HTTP, które zwraca dane w formacie JSON.

Klient `SystemZarzadzaniaZasobami` jest aplikacją konsolową, która wysyła żądania HTTP do serwera i wyświetla wyniki użytkownikowi.

Dane są przechowywane w pamięci aplikacji serwerowej. Jest to uproszczone rozwiązanie wystarczające do pokazania działania aplikacji rozproszonej.

## Uruchomienie projektu

Najpierw należy uruchomić projekt serwera:

```bash
ZasobyApi
