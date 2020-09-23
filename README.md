# TestJob

There are currently 3 branches.

Master and phantom_stones branches are mostly identical and use slightly different approaches to bomb position calculation.

Grid branch does not follow the task requirement about calculating bomb position according to the adjacent stones.
Instead I've created a grid system with an opportunity to use integer numbers to place objects.
This approach significantly simplified the whole system, excluded potential bugs and slightly increased performance.
