# A Little \*Secret\* Ingredient
Made with artistic passion!

## Prerequisites
1. A dump of Fire Emblem Engage 2.0.0
2. .NET 7 Desktop Runtime: https://dotnet.microsoft.com/en-us/download/dotnet/7.0

## How to Use
1. Launch the ALittleSecretIngredient.exe executable.
2. Input your Fire Emblem Engage dump when prompted.
3. Configure randomization settings to your \*heart's\* content.
4. Click the 'Randomize and Export' button.
5. Load the resulting mod when \*next\* you play Fire Emblem Engage.

## Available Randomizations
- Bond Level Exp Requirements
- Bond Level Bond Fragment Costs
- Character Ages
- Character Animation Swap
- Character Attributes
- Character Base Stat Modifiers
- Character Birthdays
- Character Body Shapes/Sizes
- Character Internal Levels
- Character Model Swap
- Character Mount Model Swap
- Character Outfit Color Palettes
- Character Outfit Swap
- Character Personal Skills
- Character Proficiencies
- Character Starting Classes
- Character Starting Inventories
- Character Starting Levels
- Character Starting SP
- Character Stat Growths
- Character Stat Limit Modifiers
- Character Static Starting Inventories
- Character Support Categories
- Class Attributes
- Class Base Stats
- Class Movement Types
- Class Skills
- Class Stat Growth Modifiers
- Class Stat Limits
- Class Unit Types
- Class Weapon Ranks
- Class Weapon Types
- Emblem Bond Link Pairings
- Emblem Bond Link Skills
- Emblem Engage Attack Skills
- Emblem Engage Meter Sizes
- Emblem Engage Skills
- Emblem Engage Weapons
- Emblem Engage+ Characters
- Emblem Engraving Stats
- Emblem Inheritable Skills
- Emblem Proficiency Unlocks
- Emblem Static Sync Stats
- Emblem Strong Bond and Deep Synergy Unlock Levels
- Emblem Sync Skills
- Emblem Sync Stats
- Emblem Weapon Restrictions
- Enemy Attributes
- Enemy Base Stat Modifiers
- Enemy Class Stat Growths
- Enemy Classes
- Enemy Emblems
- Enemy Extra Skills
- Enemy Inventories
- Enemy Item Drops
- Enemy Levels
- Enemy Maddening Skills
- Enemy Personal Skills
- Enemy Revival Stones
- Enemy Static Starting Inventories
- Map Deployment Slots
- Map Enemy Counts
- Map Forced Deployments
- Map Unit Positions

## About
You may have noticed that buttons are littered all over the settings windows, all of which open one of two types of windows: a numeric distribution control or a selection distribution control. These windows serve as ways for you to edit the probability distributions of each individual randomization option. If this sounds like a bother to learn and to deal with, know that it is entirely optional. All of them come pre-set with an acceptable - "mid-heat," so to speak - configuration.

### Numeric Distribution Control

![image](https://user-images.githubusercontent.com/34029571/236585597-8ccfd93c-5efe-4478-96ad-e964855bc421.png)

This kind of window has to do with how *numbers* are selected. There are several modes of distribution to select from:
- Uniform distributions select numbers evenly from min to max, like your usual random number generator without bias towards any numbers.
- Normal distributions select numbers that tend more towards the mean. This is more accurate to how most distributions actually are, and is thus more "organic." Specifically, it selects numbers that, on average, have a distance of [Standard Deviation] to the Mean.
- Redistribution just uses the previous values, but swaps them around.

Furthermore, additional modes exist for the uniform and normal distributions having to do with what role the old values play during randomization:
- Constant: Ignore the old value. Generate a new number the same way every time.
- Relative: Generate a new number the same way every time, but just *add* it to the old value instead of replacing it.
- Proportional: Generate a new number, but *multiply* it with the old value. This is much like the Relative option, but has the added property of being consistent for small numbers and more erratic for big ones.

**Why would you want the old values to play a role in randomization, making the result less random?**

Two words: game balance. You can probably envision a scenario where something like 'unit level' is randomized, and then you meet an unbeatable level 40 unit on chapter 1. Not very fun. Does that mean that unit level randomization can't be fun? Nope, it absolutely can be. Just make sure that it is randomized appropriately.

### Selection Distribution Control

![image](https://user-images.githubusercontent.com/34029571/236589497-2a7e776d-990e-4c30-8fc4-95d47cc1e9dd.png)

This one is probably a bit easier to understand. It has to do with randomizing anything that *isn't* a number. There are again some options:
- Uniform: Select stuff with equal probability. Unbiased toward any particular item. Lets you select what items can/can't be the result.
- Empirical: Select stuff based on their weight. An item with weight 2 is twice as likely/frequent as an item with weight 1.
- Redistribution: Swaps items around indiscriminately.

You may have noticed the field named 'Randomization Probability' present in both distribution control windows. This field's purpose is to determine what percentage of the data is to be randomized. A value of 100 indicates that every item/number should be randomized, and a value of 0 is effectively the same as disabling the randomization option altogether as it means that none of the items/numbers should be randomized. What's the point of this? Perhaps you want a "low-heat" randomization in a way that allows for drastic changes, but without the overall result being too insane?

### Support
This project is mostly a means of recreation for me, and I don't expect payment. That said, a donation would certainly be a welcome surprise!

https://ko-fi.com/nifyr
