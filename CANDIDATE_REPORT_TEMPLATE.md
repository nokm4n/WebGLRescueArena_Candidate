# Summary

# Environment

Unity version: 6000.5.3f1

Browser:

Hardware:

# Baseline

FPS / Frame Time: 180/4ms

Memory: 2gb

GC Alloc: 2.4gb

Build Size: 9.8mb

Load Time: 1 ms

# Issues Found
Большинство классов идут в одну строку. Нейминг приватных полей без _. Нет отступов между методами.
Ненужный Update метод в классе EnemyManager
Игровой канвас просто висит в воздухе, ни к чему не привязан, ни от какого угла не скейлится
Пули не врезаются в стенки
Обновление UI в методе Update, а не по ивентам.
Куча GetComponent, FindComponent в Update методах.


# Changes Made

В Player Rigidbody  поставил галочку freeze Pos Y, что бы при соприкосновении с врагами игрок не улетал
Переделал Projectile OnTriggerEnter под более надежную систему, убрал GetComponent Rigidbody, заменил на поле SerializeField
Убрал лишнее вычисление расстояния в EnemyAttack, вместо этого передаю уже вычисленное значение из EnemyController.
Добавил проверку на смерть игрока врагам и в GameManager, что бы после смерти игра останавливалась
У партиклов убрал столкновения.
Починил NullRefExp в главном меню, добавил меню настроек

# Measurements After Changes

FPS / Frame Time: 280/3ms

# Remaining Issues

# What I Would Do Next
Edit spawning/Destroing enemies/bullets to object pull.
Add DI instead of FindObject or SerializeFields.
Particle edit to one sprite and other optimizations.
Fix UI.
Fix Code style.

