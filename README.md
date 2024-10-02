# Лабораторна робота №1 з дисципліни «Комп’ютерні системи» «Планування задач у багатопроцесорних комп'ютерних системах»
## Загальне завдання 
1. Ознайомитись з описом лабораторної роботи.
2. Потрібно опрацювати (надати 4 набора параметрів роботи системи):
    1) за алгоритмом **FIFO**;
    2) за алгоритмом з окремим процесором-планувальником (коли найслабкіший з точки зору продуктивності процесорний елемент є планувальником);
    3) за алгоритмом, по якому функції планування покладені на найбільш потужний процесорний елемент, який періодично перериває обчислення для управління чергою. Цей процесор є планувальником, але й приймає безпосередньо участь у обчислювальному процесі. Визначити час роботи процесора над задачами як 20 мс, час на планування – 4 мс;
    4) те саме, що і у попередньому пункті із найпотужнішим процесором у якості планувальника, але визначити час роботи над задачами самостійно, виходячи з оптимальної швидкодії системи в цілому.

### Відповідь у лабораторній роботі:
- Кількість реалізованих задач (виконаних системою операцій) за 10 с.
- Треба вказати співвідношення кількості виконаних системою операцій до максимально можливої кількості операцій (своєрідний ККД системи).

**Максимально можлива кількість операцій** – це сума продуктивностей працюючих процесорів за 10 с (слід враховувати, що у пп. 3) та 4) найпотужніший процесор працює не весь час, перериваючись на функції планувальника).

**Програмний інтерфейс** бажано зробити таким, щоб він надавав можливість задавати швидкодію усіх процесорів системи, імовірність виникнення задач, границі складності задач.

## Мета
Аналіз та вивчення особливостей планування задач у багатопроцесорних обчислювальних системах.

