namespace ALittleSecretIngredient
{
    internal class GameData
    {
        private XmlParser XP { get; }
        private FileManager FM { get; }
        internal DataSet Get(DataSetEnum dse) => XP.GetData(dse);
        internal void SetDirty(DataSetEnum dse) => FM.SetDirty(XP.DataSetToSheetName[dse].fe);

        #region Bond Level IDs
        internal List<(string id, string name)> BondLevelsFromExp { get; } = new()
        {
            ("2", "Level 2"), ("3", "Level 3"), ("4", "Level 4"), ("5", "Level 5"),
            ("6", "Level 6"), ("7", "Level 7"), ("8", "Level 8"), ("9", "Level 9"),
            ("10", "Level 10"), ("11", "Level 11"), ("12", "Level 12"), ("13", "Level 13"),
            ("14", "Level 14"), ("15", "Level 15"), ("16", "Level 16"), ("17", "Level 17"),
            ("18", "Level 18"), ("19", "Level 19"), ("20", "Level 20")
        };
        internal List<(string id, string name)> BondLevels { get; } = new() // BondLevelsFromExp +
        {
            ("0", "Level 0"), ("1", "Level 1")
        }; // For consistency's sake, but that doesn't stop me from feeling like a dumbass for doing it this way.
        #endregion
        #region Bond Level Table IDs
        internal List<(string id, string name)> InheritableBondLevelTables { get; } = new()
        {
            ("GGID_マルス", "Marth"), ("GGID_シグルド", "Sigurd"), ("GGID_セリカ", "Celica"), ("GGID_ミカヤ", "Micaiah"),
            ("GGID_ロイ", "Roy"), ("GGID_リーフ", "Leif"), ("GGID_ルキナ", "Lucina"), ("GGID_リン", "Lyn"),
            ("GGID_アイク", "Ike"), ("GGID_ベレト", "Byleth"), ("GGID_カムイ", "Corrin"), ("GGID_エイリーク", "Eirika"),
            ("GGID_エーデルガルト", "Edelgard"), ("GGID_チキ", "Tiki"), ("GGID_ヘクトル", "Hector"), ("GGID_ヴェロニカ", "Veronica"),
            ("GGID_セネリオ", "Soren"), ("GGID_カミラ", "Camilla"), ("GGID_クロム", "Chrom")
        };

        internal List<(string id, string name)> AllyBondLevelTables { get; } = new() // InheritableBondLevelTables +
        {
            ("GGID_リュール", "Alear")
        };

        internal List<(string id, string name)> EnemyBondLevelTables { get; } = new()
        {
            ("GGID_M002_シグルド", "Sigurd (Chapter 2)"), ("GGID_M007_敵ルキナ", "Corrupted Lucina"),
            ("GGID_M007_敵ルキナ", "Corrupted Lucina"), ("GGID_M008_敵リーフ", "Corrupted Leif (Chapter 8)"),
            ("GGID_M010_敵ベレト", "Corrupted Byleth (Chapter 10)"), ("GGID_M010_敵リン", "Corrupted Lyn"),
            ("GGID_M011_敵マルス", "Corrupted Marth (Chapter 11)"), ("GGID_M011_敵シグルド", "Corrupted Sigurd (Chapter 11)"),
            ("GGID_M011_敵セリカ", "Corrupted Celica (Chapter 11)"), ("GGID_M011_敵ミカヤ", "Corrupted Micaiah (Chapter 11)"),
            ("GGID_M011_敵ロイ", "Corrupted Roy (Chapter 11)"), ("GGID_M011_敵リーフ", "Corrupted Leif (Chapter 11)"),
            ("GGID_M014_敵ベレト", "Corrupted Byleth (Chapter 14)"), ("GGID_M017_敵マルス", "Corrupted Marth (Chapter 17)"),
            ("GGID_M017_敵シグルド", "Corrupted Sigurd (Chapter 17)"), ("GGID_M017_敵セリカ", "Corrupted Celica (Chapter 17)"),
            ("GGID_M017_敵ミカヤ", "Corrupted Micaiah (Chapter 17)"), ("GGID_M017_敵ロイ", "Corrupted Roy (Chapter 17)"),
            ("GGID_M017_敵リーフ", "Corrupter Leif (Chapter 17)"), ("GGID_M019_敵ミカヤ", "Corrupted Micaiah (Chapter 19)"),
            ("GGID_M019_敵ロイ", "Corrupted Roy (Chapter 19)"), ("GGID_M020_敵セリカ", "Corrupted Celica (Chapter 20)"),
            ("GGID_M021_敵マルス", "Corrupted Marth (Chapter 21)"), ("GGID_M024_敵マルス", "Corrupted Marth (Chapter 24)"),
            ("GGID_E001_敵チキ", "Corrupted Tiki (Xenologue 1)"), ("GGID_E002_敵ヘクトル", "Corrupted Hector (Xenologue 2)"),
            ("GGID_E003_敵ヴェロニカ", "Corrupted Veronica (Xenologue 3)"), ("GGID_E004_敵セネリオ", "Corrupted Soren (Xenologue 4)"),
            ("GGID_E004_敵カミラ", "Corrupted Camilla (Xenologue 4)"), ("GGID_E005_敵クロム", "Corrupted Chrom (Xenologue 5)"),
            ("GGID_E005_敵ヘクトル", "Corrupted Hector (Xenologue 5)"), ("GGID_E005_敵ヴェロニカ", "Corrupted Veronica (Xenologue 5)"),
            ("GGID_E006_敵チキ", "Corrupted Tiki (Xenologue 6)"), ("GGID_E006_敵ヘクトル", "Corrupted Hector (Xenologue 6)"),
            ("GGID_E006_敵ヴェロニカ", "Corrupted Veronica (Xenologue 6)"), ("GGID_E006_敵セネリオ", "Corrupted Soren (Xenologue 6)"),
            ("GGID_E006_敵カミラ", "Corrupted Camilla (Xenologue 6)"), ("GGID_E006_敵クロム", "Corrupted Chrom (Xenologue 6)"),
            ("GGID_E006_敵エーデルガルト", "Corrupted Edelgard")
        };

        internal List<(string id, string name)> BondLevelTables { get; } = new(); // AllyBondLevelTables + EnemyBondLevelTables
        #endregion
        #region Character IDs
        internal List<(string id, string name)> PlayableCharacters { get; } = new()
        {
            ("PID_リュール", "Alear"), ("PID_ヴァンドレ", "Vander"), ("PID_クラン", "Clanne"), ("PID_フラン", "Framme"),
            ("PID_アルフレッド", "Alfred"), ("PID_エーティエ", "Etie"), ("PID_ブシュロン", "Boucheron"), ("PID_セリーヌ", "Céline"),
            ("PID_クロエ", "Chloé"), ("PID_ルイ", "Louis"), ("PID_ユナカ", "Yunaka"), ("PID_スタルーク", "Alcryst"),
            ("PID_シトリニカ", "Citrinne"), ("PID_ラピス", "Lapis"), ("PID_ディアマンド", "Diamant"), ("PID_アンバー", "Amber"),
            ("PID_ジェーデ", "Jade"), ("PID_アイビー", "Ivy"), ("PID_カゲツ", "Kagetsu"), ("PID_ゼルコバ", "Zelkov"),
            ("PID_フォガート", "Fogado"), ("PID_パンドロ", "Pandreo"), ("PID_ボネ", "Bunet"), ("PID_ミスティラ", "Timerra"),
            ("PID_パネトネ", "Panette"), ("PID_メリン", "Merrin"), ("PID_オルテンシア", "Hortensia"), ("PID_セアダス", "Seadall"),
            ("PID_ロサード", "Rosado"), ("PID_ゴルドマリー", "Goldmary"), ("PID_リンデン", "Lindon"), ("PID_ザフィーア", "Saphir"),
            ("PID_ヴェイル", "Veyle"), ("PID_モーヴ", "Mauvier"), ("PID_アンナ", "Anna"), ("PID_ジャン", "Jean"),
            ("PID_エル", "Nel"), ("PID_ラファール", "Rafal"), ("PID_セレスティア", "Zelestia"), ("PID_グレゴリー", "Gregory"),
            ("PID_マデリーン", "Madeline")
        };
        #endregion
        #region DemoAnim IDs
        internal List<(string id, string name)> UniqueMaleDemoAnims { get; } = new()
        {
            ("AOC_Demo_c001", "Male Alear"), ("AOC_Demo_c049", "Rafal A"),
            ("AOC_Demo_c049b", "Rafal B"), ("AOC_Demo_c100", "Alfred"),
            ("AOC_Demo_c101", "Boucheron"), ("AOC_Demo_c102", "Louis"),
            ("AOC_Demo_c103", "Jean"), ("AOC_Demo_c200", "Diamant"),
            ("AOC_Demo_c201", "Alcryst"), ("AOC_Demo_c202", "Morion"),
            ("AOC_Demo_c203", "Amber"), ("AOC_Demo_c300", "Hyacinth"),
            ("AOC_Demo_c301", "Zelkov"), ("AOC_Demo_c302", "Kagetsu"),
            ("AOC_Demo_c304", "Lindon"), ("AOC_Demo_c400", "Fogado"),
            ("AOC_Demo_c401", "Pandreo"), ("AOC_Demo_c402", "Bunet"),
            ("AOC_Demo_c403", "Seadall"), ("AOC_Demo_c500", "Vander"),
            ("AOC_Demo_c501", "Clanne"), ("AOC_Demo_c502", "Mauvier"),
            ("AOC_Demo_c503", "Griss"), ("AOC_Demo_c503b", "Gregory"),
            ("AOC_Demo_God0M", "Male Emblem"), ("AOC_Demo_c514", "Dimitri"),
            ("AOC_Demo_c515", "Claude"), ("AOC_Demo_c510", "Hector"),
            ("AOC_Demo_c511", "Soren"), ("AOC_Demo_c512", "Chrom"),
            ("AOC_Demo_c513", "Robin"),
        };

        internal List<(string id, string name)> GenericMaleDemoAnims { get; } = new()
        {
            ("AOC_Demo_Hum0M", "Male A"), ("AOC_Demo_Hum1M", "Male B"),
            ("AOC_Demo_Hum2M", "Male C"), ("AOC_Demo_c702", "Corrupted Male"),
            ("AOC_Demo_c809", "Old Man"),
        };

        internal List<(string id, string name)> UniqueFemaleDemoAnims { get; } = new()
        {
            ("AOC_Demo_c051", "Female Alear"), ("AOC_Demo_c099", "Nel"),
            ("AOC_Demo_c150", "Céline"), ("AOC_Demo_c151", "Éve"),
            ("AOC_Demo_c152", "Etie"), ("AOC_Demo_c153", "Chloé"),
            ("AOC_Demo_c250", "Jade"), ("AOC_Demo_c251", "Lapis"),
            ("AOC_Demo_c252", "Citrinne"), ("AOC_Demo_c253", "Yunaka"),
            ("AOC_Demo_c254", "Saphir"), ("AOC_Demo_c303", "Rosado"),
            ("AOC_Demo_c350", "Ivy"), ("AOC_Demo_c351", "Hortensia"),
            ("AOC_Demo_c352", "Goldmary"), ("AOC_Demo_c450", "Timerra"),
            ("AOC_Demo_c451", "Seforia"), ("AOC_Demo_c452", "Merrin"),
            ("AOC_Demo_c453", "Panette"), ("AOC_Demo_c550", "Framme"),
            ("AOC_Demo_c551", "Veyle"), ("AOC_Demo_c556", "Evil Veyle"),
            ("AOC_Demo_c552", "Anna"), ("AOC_Demo_c553", "Zephia"),
            ("AOC_Demo_c553b", "Zelestia"), ("AOC_Demo_c554", "Marni"),
            ("AOC_Demo_c554b", "Madeline"), ("AOC_Demo_c555", "Lumera"),
            ("AOC_Demo_God0F", "Female Emblem"), ("AOC_Demo_c560", "Tiki"),
            ("AOC_Demo_c563", "Edelgard"), ("AOC_Demo_c561", "Camilla"),
            ("AOC_Demo_c562", "Veronica"),
        };

        internal List<(string id, string name)> GenericFemaleDemoAnims { get; } = new()
        {
            ("AOC_Demo_Hum0F", "Female A"), ("AOC_Demo_Hum1F", "Female B"),
            ("AOC_Demo_Hum2F", "Female C"), ("AOC_Demo_Hum3F", "Female D"),
            ("AOC_Demo_Hum0FL", "Female E"), ("AOC_Demo_Hum1FL", "Female F"),
            ("AOC_Demo_Hum2FL", "Female G"), ("AOC_Demo_Hum3FL", "Female H"),
            ("AOC_Demo_c703", "Corrupted Female"),
        };
        #endregion
        #region Dress Model IDs
        internal List<(string id, string name)> MaleClassDressModels { get; } = new()
        {
            ("uBody_Swd0AM_c000", "Male Sword Fighter"), ("uBody_Swd1AM_c699", "Male Swordmaster"),
            ("uBody_Swd1AM_c000", "Male Enemy Swordmaster"), ("uBody_Swd2AM_c000", "Male Hero"),
            ("uBody_Lnc0AM_c000", "Male Lance Fighter"), ("uBody_Lnc1AM_c000", "Male Halberdier"),
            ("uBody_Lnc2BM_c000", "Male Royal Knight"), ("uBody_Axe0AM_c699", "Male Axe Fighter"),
            ("uBody_Axe0AM_c000", "Male Enemy Axe Fighter"), ("uBody_Axe1AM_c699", "Male Berserker A"),
            ("uBody_Axe1AM_c699b", "Male Berserker B"), ("uBody_Axe1AM_c699c", "Male Berserker C"),
            ("uBody_Axe1AM_c699d", "Male Berserker D"), ("uBody_Axe1AM_c000", "Male Enemy Berserker"),
            ("uBody_Axe2AM_c000", "Male Warrior A"), ("uBody_Axe2AM_c000b", "Male Warrior B"),
            ("uBody_Axe2AM_c000c", "Male Warrior C"), ("uBody_Axe2AM_c000d", "Male Warrior D"),
            ("uBody_Amr0AM_c699", "Male Sword/Lance/Axe Armor"), ("uBody_Amr0AM_c000", "Male Enemy Sword/Lance/Axe Armor"),
            ("uBody_Amr1AM_c699", "Male General"), ("uBody_Amr1AM_c000", "Male Enemy General"),
            ("uBody_Amr2BM_c699", "Male Great Knight"), ("uBody_Amr2BM_c000", "Male Enemy Great Knight"),
            ("uBody_Bow0AM_c699", "Male Archer"), ("uBody_Bow0AM_c000", "Male Enemy Archer"),
            ("uBody_Bow1AM_c699", "Male Sniper"), ("uBody_Bow1AM_c000", "Male Enemy Sniper"),
            ("uBody_Bow2BM_c000", "Male Bow Knight"), ("uBody_Cav0BM_c000", "Male Sword/Lance/Axe Cavalier"),
            ("uBody_Cav1BM_c000", "Male Paladin"), ("uBody_Cav2CM_c000", "Male Wolf Knight"),
            ("uBody_Wng1FM_c000", "Male Griffin Knight"), ("uBody_Wng2DM_c000", "Male Wyvern Knight"),
            ("uBody_Dge0AM_c699", "Male Thief A"), ("uBody_Dge0AM_c699d", "Male Thief B"),
            ("uBody_Dge0AM_c000", "Male Enemy Thief"), ("uBody_Mag0AM_c000", "Male Mage"),
            ("uBody_Mag1AM_c000", "Male Sage A"), ("uBody_Mag1AM_c000b", "Male Sage B"),
            ("uBody_Mag1AM_c000l", "Male Sage C"), ("uBody_Mag1AM_c000c", "Male Sage D"),
            ("uBody_Mag1AM_c000d", "Male Sage E"), ("uBody_Mag1AM_c699", "Male Enemy Sage"),
            ("uBody_Mag2BM_c000", "Male Mage Knight"), ("uBody_Rod0AM_c000", "Male Martial Monk"),
            ("uBody_Rod1AM_c000", "Male Martial Master A"), ("uBody_Rod1AM_c000b", "Male Martial Master B"),
            ("uBody_Rod1AM_c000c", "Male Martial Master C"), ("uBody_Rod1AM_c000d", "Male Martial Master D"),
            ("uBody_Rod2AM_c000", "Male High Priest"), ("uBody_Bbr0AM_c000", "Male Barbarian"),
            ("uBody_Ect3AM_c000", "Male Enchanter"), ("uBody_Mcn3AM_c000", "Male Mage Cannoneer"),
            ("uBody_File4M_c809", "Male Firenese Villager"),
        };

        internal List<(string id, string name)> MaleCorruptedClassDressModels { get; } = new()
        {
            ("uBody_Swd0AM_c702", "Male Corrupted Sword Fighter"), ("uBody_Swd1AM_c704", "Male Corrupted Swordmaster"),
            ("uBody_Swd2AM_c704", "Male Corrupted Hero"), ("uBody_Lnc0AM_c702", "Male Corrupted Lance Fighter"),
            ("uBody_Lnc1AM_c704", "Male Corrupted Halberdier"), ("uBody_Lnc2BM_c704", "Male Corrupted Royal Knight"),
            ("uBody_Axe0AM_c702", "Male Corrupted Axe Fighter"), ("uBody_Axe1AM_c704", "Male Corrupted Berserker"),
            ("uBody_Axe2AM_c704", "Male Corrupted Warrior"), ("uBody_Amr0AM_c702", "Male Corrupted Sword/Lance/Axe Armor"),
            ("uBody_Amr1AM_c704", "Male Corrupted General"), ("uBody_Amr2BM_c704", "Male Corrupted Great Knight"),
            ("uBody_Bow0AM_c702", "Male Corrupted Archer"), ("uBody_Bow1AM_c704", "Male Corrupted Sniper"),
            ("uBody_Bow2BM_c704", "Male Corrupted Bow Knight"), ("uBody_Cav0BM_c702", "Male Corrupted Sword/Lance/Axe Cavalier"),
            ("uBody_Cav1BM_c704", "Male Corrupted Paladin"), ("uBody_Cav2CM_c704", "Male Corrupted Wolf Knight"),
            ("uBody_Wng1FM_c704", "Male Corrupted Griffin Knight"), ("uBody_Wng2DM_c704", "Male Corrupted Wyvern Knight"),
            ("uBody_Dge0AM_c702", "Male Corrupted Thief"), ("uBody_Mag0AM_c702", "Male Corrupted Mage"),
            ("uBody_Mag1AM_c704", "Male Corrupted Sage"), ("uBody_Mag2BM_c704", "Male Corrupted Mage Knight"),
            ("uBody_Rod0AM_c702", "Male Corrupted Martial Monk"), ("uBody_Rod1AM_c704", "Male Corrupted Marial Master"),
            ("uBody_Rod2AM_c704", "Male Corrupted High Priest"), ("uBody_Bbr0AM_c702", "Male Corrupted Barbarian"),
            ("uBody_Ect3AM_c704", "Male Corrupted Enchanter"), ("uBody_Mcn3AM_c704", "Male Corrupted Mage Cannoneer"),
        };

        internal List<(string id, string name)> FemaleClassDressModels { get; } = new()
        {
            ("uBody_Swd0AF_c699", "Female Sword Fighter"), ("uBody_Swd0AF_c000", "Female Enemy Sword Fighter"),
            ("uBody_Swd1AF_c699", "Female Swordmaster"), ("uBody_Swd1AF_c000", "Female Enemy Swordmaster"), 
            ("uBody_Swd2AF_c000", "Female Hero"), ("uBody_Lnc0AF_c000", "Female Lance Fighter"), 
            ("uBody_Lnc1AF_c000", "Female Halberdier"), ("uBody_Lnc2BF_c000", "Female Royal Knight"),
            ("uBody_Axe0AF_c699", "Female Axe Fighter"), ("uBody_Axe0AF_c000", "Female Enemy Axe Fighter"),
            ("uBody_Axe1AF_c699", "Female Berserker A"), ("uBody_Axe1AF_c699b", "Female Berserker B"),
            ("uBody_Axe1AF_c699c", "Female Berserker C"), ("uBody_Axe1AF_c699d", "Female Berserker D"),
            ("uBody_Axe1AF_c000", "Female Enemy Berserker"), ("uBody_Axe2AF_c000", "Female Warrior A"),
            ("uBody_Axe2AF_c000b", "Female Warrior B"), ("uBody_Axe2AF_c000c", "Female Warrior C"),
            ("uBody_Axe2AF_c000d", "Female Warrior D"), ("uBody_Amr0AF_c699", "Female Sword/Lance/Axe Armor"),
            ("uBody_Amr0AF_c000", "Female Enemy Sword/Lance/Axe Armor"), ("uBody_Amr1AF_c699", "Female General"),
            ("uBody_Amr1AF_c000", "Female Enemy General"), ("uBody_Amr2BF_c699", "Female Great Knight"),
            ("uBody_Amr2BF_c000", "Female Enemy Great Knight"), ("uBody_Bow0AF_c699", "Female Archer"),
            ("uBody_Bow0AF_c000", "Female Enemy Archer"), ("uBody_Bow1AF_c699", "Female Sniper"),
            ("uBody_Bow1AF_c000", "Female Enemy Sniper"), ("uBody_Bow2BF_c000", "Female Bow Knight"),
            ("uBody_Cav0BF_c000", "Female Sword/Lance/Axe Cavalier"), ("uBody_Cav1BF_c000", "Female Paladin"),
            ("uBody_Cav2CF_c000", "Female Wolf Knight"), ("uBody_Wng0EF_c000", "Female Sword/Lance/Axe Flier"),
            ("uBody_Wng1FF_c000", "Female Griffin Knight"), ("uBody_Wng2DF_c000", "Female Wyvern Knight"),
            ("uBody_Dge0AF_c699", "Female Thief A"), ("uBody_Dge0AF_c699d", "Female Thief B"),
            ("uBody_Dge0AF_c000", "Female Enemy Thief"), ("uBody_Mag0AF_c000", "Female Mage A"),
            ("uBody_Mag0AF_c000l", "Female Mage B"), ("uBody_Mag1AF_c000", "Female Sage A"),
            ("uBody_Mag1AF_c000l", "Female Sage B"), ("uBody_Mag1AF_c000b", "Female Sage C"),
            ("uBody_Mag1AF_c000c", "Female Sage D"), ("uBody_Mag1AF_c000d", "Female Sage E"),
            ("uBody_Mag1AF_c699", "Female Enemy Sage"), ("uBody_Mag2BF_c000", "Female Mage Knight A"),
            ("uBody_Mag2BF_c000l", "Female Mage Knight B"), ("uBody_Rod0AF_c000", "Female Martial Monk"),
            ("uBody_Rod1AF_c000", "Female Martial Master"), ("uBody_Rod2AF_c000", "Female High Priest"),
            ("uBody_Bbr0AF_c000", "Female Barbarian"), ("uBody_Ect3AF_c000", "Female Enchanter"),
            ("uBody_Mcn3AF_c000", "Female Mage Cannoneer"),
        };

        internal List<(string id, string name)> FemaleCorruptedClassDressModels { get; } = new()
        {
            ("uBody_Swd0AF_c703", "Female Corrupted Sword Fighter"), ("uBody_Swd1AF_c705", "Female Corrupted Swordmaster"),
            ("uBody_Swd2AF_c705", "Female Corrupted Hero"), ("uBody_Lnc0AF_c703", "Female Corrupted Lance Fighter"),
            ("uBody_Lnc1AF_c705", "Female Corrupted Halberdier"), ("uBody_Lnc2BF_c705", "Female Corrupted Royal Knight"),
            ("uBody_Axe0AF_c703", "Female Corrupted Axe Fighter"), ("uBody_Axe1AF_c705", "Female Corrupted Berserker"),
            ("uBody_Axe2AF_c705", "Female Corrupted Warrior"), ("uBody_Amr0AF_c703", "Female Corrupted Sword/Lance/Axe Armor"),
            ("uBody_Amr1AF_c705", "Female Corrupted General"), ("uBody_Amr2BF_c705", "Female Corrupted Great Knight"),
            ("uBody_Bow0AF_c703", "Female Corrupted Archer"), ("uBody_Bow1AF_c705", "Female Corrupted Sniper"),
            ("uBody_Bow2BF_c705", "Female Corrupted Bow Knight"), ("uBody_Cav0BF_c703", "Female Corrupted Sword/Lance/Axe Cavalier"),
            ("uBody_Cav1BF_c705", "Female Corrupted Paladin"), ("uBody_Cav2CF_c705", "Female Corrupted Wolf Knight"),
            ("uBody_Wng0EF_c703", "Female Corrupted Sword/Lance/Axe Flier"), ("uBody_Wng1FF_c705", "Female Corrupted Griffin Knight"),
            ("uBody_Wng2DF_c705", "Female Corrupted Wyvern Knight"), ("uBody_Dge0AF_c703", "Female Corrupted Thief"),
            ("uBody_Mag0AF_c703", "Female Corrupted Mage"), ("uBody_Mag1AF_c705", "Female Corrupted Sage"),
            ("uBody_Mag2BF_c705", "Female Corrupted Mage Knight"), ("uBody_Rod0AF_c703", "Female Corrupted Marital Monk"),
            ("uBody_Rod1AF_c705", "Female Corrupted Martial Master"), ("uBody_Rod2AF_c705", "Female Corrupted High Priest"),
            ("uBody_Bbr0AF_c703", "Female Corrupted Barbarian"), ("uBody_Ect3AF_c705", "Female Corrupted Enchanter"),
            ("uBody_Mcn3AF_c705", "Female Corrupted Mage Cannoneer"),
        };

        internal List<(string id, string name)> MalePersonalDressModels { get; } = new()
        {
            ("uBody_Drg0AM_c001", "Male Dragon Child"), ("uBody_Drg1AM_c001", "Male Divine Dragon (Alear)"),
            ("uBody_Drg0AM_c002", "Male Fell Child (Past Alear)"), ("uBody_Sds0AM_c049", "Fell Child (Rafal)"),
            ("uBody_Avn0BM_c100", "Noble (Alfred)"), ("uBody_Avn1BM_c100", "Avenir"),
            ("uBody_Axe0AM_c101", "Axe Fighter (Boucheron)"), ("uBody_Amr0AM_c102", "Sword/Lance/Axe Armor (Louis)"),
            ("uBody_Rod0AM_c103", "Martial Monk (Jean)"), ("uBody_Scs0AM_c200", "Lord (Diamant)"),
            ("uBody_Scs1AM_c200", "Successeur"), ("uBody_Trl0AM_c201", "Lord (Alcryst)"),
            ("uBody_Trl1AM_c201", "Tireur d'élite"), ("uBody_Swd2AM_c202", "Morion"),
            ("uBody_Cav0BM_c203", "Sword/Lance/Axe Cavalier (Amber)"), ("uBody_Rod1AM_c300", "Hyacinth"),
            ("uBody_Dge0AM_c301", "Thief (Zelkov)"), ("uBody_Swd1AM_c302", "Swordmaster (Kagetsu)"),
            ("uBody_Mag1AM_c304", "Sage (Lindon)"), ("uBody_Cpd0BM_c400", "Sentinel (Fogado)"),
            ("uBody_Cpd1BM_c400", "Cupido"), ("uBody_Mag0AM_c400", "Mage (Fogado)"),
            ("uBody_Dct0AM_c400", "Wolf Knight (Fogado)"), ("uBody_Rod2AM_c401", "High Priest (Pandreo)"),
            ("uBody_Amr2BM_c402", "Great Knight (Bunet)"), ("uBody_Dnc0AM_c403", "Dancer"),
            ("uBody_Cav1BM_c500", "Paladin (Vander)"), ("uBody_Mag0AM_c501", "Mage (Clanne)"),
            ("uBody_Lnc2BM_c502", "Royal Knight (Mauvier)"), ("uBody_Mag1AM_c503", "Sage (Griss)"),
            ("uBody_Mag1AM_c503b", "Sage (Gregory)"), ("uBody_Amr0AM_c811", "Rodine"),
            ("uBody_Axe0AM_c812", "Nelucce"),
            ("uBody_Bbr0AM_c813", "Tetchie"), ("uBody_Bbr0AM_c814", "Totchie"),
            ("uBody_Rod0AM_c819", "Sean"), ("uBody_File4M_c817", "Durthon"),
            ("uBody_Brod4M_c818", "Pinet"), ("uBody_WearM_c001", "Male Alear Casual"),
            ("uBody_WearM_c100", "Alfred Casual"), ("uBody_WearM_c101", "Boucheron Casual"),
            ("uBody_WearM_c102", "Louis Casual"), ("uBody_WearM_c103", "Jean Casual"),
            ("uBody_WearM_c200", "Diamant Casual"), ("uBody_WearM_c201", "Alcryst Casual"),
            ("uBody_WearM_c203", "Amber Casual"), ("uBody_WearM_c301", "Zelkov Casual"),
            ("uBody_WearM_c302", "Kagetsu Casual"), ("uBody_WearM_c304", "Lindon Casual"),
            ("uBody_WearM_c400", "Fogado Casual"), ("uBody_WearM_c401", "Pandreo Casual"),
            ("uBody_WearM_c402", "Bunet Casual"), ("uBody_WearM_c403", "Seadall Casual"),
            ("uBody_WearM_c500", "Vander Casual"), ("uBody_WearM_c501", "Clanne Casual"),
            ("uBody_WearM_c502", "Mauvier Casual"), ("uBody_WearM_c049", "Rafal Casual"),
            ("uBody_WearM_c503", "Gregory Casual"),
        };

        internal List<(string id, string name)> FemalePersonalDressModels { get; } = new()
        {
            ("uBody_Drg0AF_c051", "Female Dragon Child"),
            ("uBody_Drg1AF_c051", "Female Divine Dragon (Alear)"), ("uBody_Drg0AF_c052", "Female Fell Child (Past Alear)"),
            ("uBody_Sds0AF_c099", "Fell Child (Nel)"), ("uBody_Flr0AF_c150", "Noble (Céline)"),
            ("uBody_Flr1AF_c150", "Vidame"), ("uBody_Rod2AF_c151", "Éve"),
            ("uBody_Bow0AF_c152", "Archer (Etie)"), ("uBody_Wng0EF_c153", "Sword/Lance/Axe Flier (Chloé)"),
            ("uBody_Amr0AF_c250", "Sword/Lance/Axe Armor (Jade)"), ("uBody_Swd0AF_c251", "Sword Fighter (Lapis)"),
            ("uBody_Mag0AF_c252", "Mage (Citrinne)"), ("uBody_Dge0AF_c253", "Thief (Yunaka)"),
            ("uBody_Axe2AF_c254", "Warrior (Saphir)"), ("uBody_Wng2DF_c303", "Wyvern Knight (Rosado)"),
            ("uBody_Lnd0DF_c350", "Wing Tamer (Ivy)"), ("uBody_Lnd1DF_c350", "Lindwurm"),
            ("uBody_Slp0EF_c351", "Wing Tamer (Hortensia)"), ("uBody_Slp1EF_c351", "Sleipnir Rider"),
            ("uBody_Swd2AF_c352", "Hero (Goldmary)"), ("uBody_Pcf0AF_c450", "Sentinel (Timerra)"),
            ("uBody_Pcf1AF_c450", "Picket"), ("uBody_Cav2CF_c450", "Wolf Knight (Timerra)"),
            ("uBody_Swd2AF_c450", "Hero (Timerra)"), ("uBody_Wng2DF_c451", "Seforia"),
            ("uBody_Cav2CF_c452", "Wolf Knight (Merrin)"), ("uBody_Axe1AF_c453", "Berserker (Panette)"),
            ("uBody_Rod0AF_c550", "Martial Monk (Framme)"), ("uBody_Sdp0AF_c551", "Fell Child (Veyle)"),
            ("uBody_Sdp0AF_c556", "Dark Veyle"), ("uBody_Sdp0AF_c557", "Hooded Veyle"),
            ("uBody_Axe0AF_c552", "Axe Fighter (Anna)"), ("uBody_Msn0DF_c553", "Melusine (Zephia)"),
            ("uBody_Msn0DF_c553b", "Melusine (Zelestia)"), ("uBody_Amr1AF_c554", "General (Marni)"),
            ("uBody_Amr1AF_c554b", "General (Madeline)"), ("uBody_Drg1AF_c555", "Divine Dragon (Lumera)"),
            ("uBody_Axe1AF_c855", "Abyme (Chapter 18)"), ("uBody_Bbr0AF_c859", "Mitan"),
            ("uBody_File4F_c858", "Anje"), ("uBody_File4F_c856", "Anisse"),
            ("uBody_Brod4F_c857", "Calney"), ("uBody_WearF_c303", "Rosado Casual"),
            ("uBody_WearF_c051", "Female Alear Casual"), ("uBody_WearF_c150", "Céline Casual"),
            ("uBody_WearF_c152", "Etie Casual"), ("uBody_WearF_c153", "Chloé Casual"),
            ("uBody_WearF_c250", "Jade Casual"), ("uBody_WearF_c251", "Lapis Casual"),
            ("uBody_WearF_c252", "Citrinne Casual"), ("uBody_WearF_c253", "Yunaka Casual"),
            ("uBody_WearF_c254", "Saphir Casual"), ("uBody_WearF_c350", "Ivy Casual"),
            ("uBody_WearF_c351", "Hortensia Casual"), ("uBody_WearF_c352", "Goldmary Casual"),
            ("uBody_WearF_c450", "Timerra Casual"), ("uBody_WearF_c452", "Merrin Casual"),
            ("uBody_WearF_c453", "Panette Casual"), ("uBody_WearF_c550", "Framme Casual"),
            ("uBody_WearF_c551", "Veyle Casual"), ("uBody_WearF_c552", "Anna Casual"),
            ("uBody_WearF_c099", "Nel Casual"), ("uBody_WearF_c553", "Zelestia Casual"),
            ("uBody_WearF_c554", "Madeline Casual"),
        };

        internal List<(string id, string name)> MaleEmblemDressModels { get; } = new()
        {
            ("uBody_Mar0AM_c530", "Marth"), ("uBody_Mar0AM_c537", "Corrupted Marth"),
            ("uBody_Sig0BM_c531", "Sigurd"), ("uBody_Sig0BM_c538", "Corrupted Sigurd"),
            ("uBody_Lei0AM_c532", "Leif"), ("uBody_Lei0AM_c539", "Corrupted Leif"),
            ("uBody_Roy0AM_c533", "Roy"), ("uBody_Roy0AM_c540", "Corrupted Roy"),
            ("uBody_Ike0AM_c534", "Ike"), ("uBody_Ike0AM_c541", "Corrupted Ike"),
            ("uBody_Byl0AM_c535", "Byleth"), ("uBody_Byl0AM_c542", "Corrupted Byleth"),
            ("uBody_Eph0AM_c536", "Ephraim"), ("uBody_Eph0AM_c543", "Corrupted Ephraim"),
            ("uBody_Drg0AM_c003", "Male Emblem Alear"), ("uBody_Dim0AM_c514", "Dimitri"),
            ("uBody_Dim0AM_c521", "Corrupted Dimitri"), ("uBody_Cla0AM_c515", "Claude"),
            ("uBody_Cla0AM_c522", "Corrupted Claude"), ("uBody_Hec0AM_c510", "Hector"),
            ("uBody_Hec0AM_c517", "Corrupted Hector"), ("uBody_Sor0AM_c511", "Soren"),
            ("uBody_Sor0AM_c518", "Corrupted Soren"), ("uBody_Chr0AM_c512", "Chrom"),
            ("uBody_Chr0AM_c519", "Corrupted Chrom"), ("uBody_Rbi0AM_c513", "Robin"),
            ("uBody_Rbi0AM_c520", "Corrupted Robin"),
        };

        internal List<(string id, string name)> FemaleEmblemDressModels { get; } = new()
        {
            ("uBody_Cel0AF_c580", "Celica"), ("uBody_Cel0AF_c587", "Corrupted Celica"),
            ("uBody_Lyn0AF_c581", "Lyn"), ("uBody_Lyn0AF_c588", "Corrupted Lyn"),
            ("uBody_Eir0AF_c582", "Eirika"), ("uBody_Eir0AF_c589", "Corrupted Eirika"),
            ("uBody_Mic0AF_c583", "Micaiah"), ("uBody_Mic0AF_c590", "Corrupted Micaiah"),
            ("uBody_Luc0AF_c584", "Lucina"), ("uBody_Luc0AF_c591", "Corrupted Lucina"),
            ("uBody_Cor0AF_c585", "Corrin"), ("uBody_Cor0AF_c592", "Corrupted Corrin"),
            ("uBody_Drg0AF_c053", "Female Emblem Alear"), ("uBody_Tik0AF_c560", "Tiki"),
            ("uBody_Tik0AF_c567", "Corrupted Tiki"), ("uBody_Ede0AF_c563", "Edelgard"),
            ("uBody_Ede0AF_c570", "Corrupted Edelgard"), ("uBody_Cmi0DF_c561", "Camilla"),
            ("uBody_Cmi0DF_c568", "Corrupted Camilla"), ("uBody_Ver0AF_c562", "Veronica"),
            ("uBody_Ver0AF_c569", "Corrupted Veronica"),
        };

        internal List<(string id, string name)> MaleEngageDressModels { get; } = new()
        {
            ("uBody_Mar1AM_c000", "Male Engaged (Marth)"), ("uBody_Sig1AM_c000", "Male Engaged (Sigurd)"),
            ("uBody_Lei1AM_c000", "Male Engaged (Leif)"), ("uBody_Roy1AM_c000", "Male Engaged (Roy)"),
            ("uBody_Ike1AM_c000", "Male Engaged (Ike)"), ("uBody_Byl1AM_c000", "Male Engaged (Byleth)"),
            ("uBody_Cel1AM_c000", "Male Engaged (Celica)"), ("uBody_Lyn1AM_c000", "Male Engaged (Lyn)"),
            ("uBody_Eir1AM_c000", "Male Engaged (Eirika)"), ("uBody_Mic1AM_c000", "Male Engaged (Micaiah)"),
            ("uBody_Luc1AM_c000", "Male Engaged (Lucina)"), ("uBody_Cor1AM_c000", "Male Engaged (Corrin)"),
            ("uBody_Ler1AM_c000", "Male Engaged (Alear)"),
            ("uBody_Thr1AM_c000", "Male Engaged (Edelgard)"), ("uBody_Hec1AM_c000", "Male Engaged (Hector)"),
            ("uBody_Sor1AM_c000", "Male Engaged (Soren)"), ("uBody_Cmi1AM_c000", "Male Engaged (Camilla)"),
            ("uBody_Ver1AM_c000", "Male Engaged (Veronica)"), ("uBody_Chr1AM_c000", "Male Engaged (Chrom)"),
        };

        internal List<(string id, string name)> FemaleEngageDressModels { get; } = new()
        {
            ("uBody_Mar1AF_c000", "Female Engaged (Marth)"), ("uBody_Sig1AF_c000", "Female Engaged (Sigurd)"),
            ("uBody_Lei1AF_c000", "Female Engaged (Leif)"), ("uBody_Roy1AF_c000", "Female Engaged (Roy)"),
            ("uBody_Ike1AF_c000", "Female Engaged (Ike)"), ("uBody_Byl1AF_c000", "Female Engaged (Byleth)"),
            ("uBody_Cel1AF_c000", "Female Engaged (Celica)"), ("uBody_Lyn1AF_c000", "Female Engaged (Lyn)"),
            ("uBody_Eir1AF_c000", "Female Engaged (Eirika)"), ("uBody_Mic1AF_c000", "Female Engaged (Micaiah)"),
            ("uBody_Luc1AF_c000", "Female Engaged (Lucina)"), ("uBody_Cor1AF_c000", "Female Engaged (Corrin)"),
            ("uBody_Ler1AF_c000", "Female Engaged (Alear)"),
            ("uBody_Thr1AF_c000", "Female Engaged (Edelgard)"), ("uBody_Hec1AF_c000", "Female Engaged (Hector)"),
            ("uBody_Sor1AF_c000", "Female Engaged (Soren)"), ("uBody_Cmi1AF_c000", "Female Engaged (Camilla)"),
            ("uBody_Ver1AF_c000", "Female Engaged (Veronica)"), ("uBody_Chr1AF_c000", "Female Engaged (Chrom)"),
        };

        internal List<(string id, string name)> MaleCommonDressModels { get; } = new()
        {
            ("uBody_File1M_c000", "Male Firene Formal 1"), ("uBody_File2M_c000", "Male Firene Formal 2"),
            ("uBody_File3M_c000", "Male Firene Formal 3"), ("uBody_File4M_c000", "Male Firene Casual 1"),
            ("uBody_File5M_c000", "Male Firene Casual 2"), ("uBody_File6M_c000", "Male Firene Casual 3"),
            ("uBody_Brod1M_c000", "Male Brodia Formal 1"), ("uBody_Brod2M_c000", "Male Brodia Formal 2"),
            ("uBody_Brod3M_c000", "Male Brodia Formal 3"), ("uBody_Brod4M_c000", "Male Brodia Casual 1"),
            ("uBody_Brod5M_c000", "Male Brodia Casual 2"), ("uBody_Brod6M_c000", "Male Brodia Casual 3"),
            ("uBody_Irci1M_c000", "Male Elusia Formal 1"), ("uBody_Irci2M_c000", "Male Elusia Formal 2"),
            ("uBody_Irci3M_c000", "Male Elusia Formal 3"), ("uBody_Irci4M_c000", "Male Elusia Casual 1"),
            ("uBody_Irci5M_c000", "Male Elusia Casual 2"), ("uBody_Irci6M_c000", "Male Elusia Casual 3"),
            ("uBody_Solu1M_c000", "Male Solm Formal 1"), ("uBody_Solu2M_c000", "Male Solm Formal 2"),
            ("uBody_Solu3M_c000", "Male Solm Formal 3"), ("uBody_Solu4M_c000", "Male Solm Casual 1"),
            ("uBody_Solu5M_c000", "Male Solm Casual 2"), ("uBody_Solu6M_c000", "Male Solm Casual 3"),
            ("uBody_Lith1M_c000", "Male Lythos 1"), ("uBody_Lith2M_c000", "Male Lythos 2"),
            ("uBody_Lith3M_c000", "Male Lythos 3"), ("uBody_SwimM1_c000", "Male Swimwear 1A"),
            ("uBody_SwimM1_c000b", "Male Swimwear 1B"), ("uBody_SwimM1_c000c", "Male Swimwear 1C"),
            ("uBody_SwimM1_c000d", "Male Swimwear 1D"), ("uBody_SwimM2_c000", "Male Swimwear 2A"),
            ("uBody_SwimM2_c000b", "Male Swimwear 2B"), ("uBody_SwimM2_c000c", "Male Swimwear 2C"),
            ("uBody_SwimM2_c000d", "Male Swimwear 2D"), ("uBody_SwimM3_c000", "Male Swimwear 3A"),
            ("uBody_SwimM3_c000b", "Male Swimwear 3B"), ("uBody_SwimM3_c000c", "Male Swimwear 3C"),
            ("uBody_SwimM3_c000d", "Male Swimwear 3D"), ("uBody_ExerM1_c000", "Male Training Wear A"),
            ("uBody_ExerM1_c000c", "Male Training Wear B"), ("uBody_ExerM1_c000d", "Male Training Wear C"),
            ("uBody_KimnM_c000", "Male Kimono"), ("uBody_CstmM_c000", "Male Sommie Costume A"),
            ("uBody_CstmM_c699", "Male Sommie Costume B"), ("uBody_Mar0AM_c000", "Marth Costume"),
            ("uBody_Sig0BM_c000", "Sigurd Costume"), ("uBody_Roy0AM_c000", "Roy Costume"),
            ("uBody_Ike0AM_c000", "Ike Costume A"), ("uBody_Ike0AM_c000b", "Ike Costume B"),
            ("uBody_Lei0AM_c000", "Leif Costume"), ("uBody_Byl0AM_c000", "Byleth Costume"),
        };

        internal List<(string id, string name)> FemaleCommonDressModels { get; } = new()
        {
            ("uBody_File1F_c000", "Female Firene Formal 1"), ("uBody_File2F_c000", "Female Firene Formal 2"),
            ("uBody_File3F_c000", "Female Firene Formal 3"), ("uBody_File4F_c000", "Female Firene Casual 1"),
            ("uBody_File5F_c000", "Female Firene Casual 2"), ("uBody_File6F_c000", "Female Firene Casual 3"),
            ("uBody_Brod1F_c000", "Female Brodia Formal 1"), ("uBody_Brod2F_c000", "Female Brodia Formal 2"),
            ("uBody_Brod3F_c000", "Female Brodia Formal 3"), ("uBody_Brod4F_c000", "Female Brodia Casual 1"),
            ("uBody_Brod5F_c000", "Female Brodia Casual 2"), ("uBody_Brod6F_c000", "Female Brodia Casual 3"),
            ("uBody_Irci1F_c000", "Female Elusia Formal 1"), ("uBody_Irci2F_c000", "Female Elusia Formal 2"),
            ("uBody_Irci3F_c000", "Female Elusia Formal 3"), ("uBody_Irci4F_c000", "Female Elusia Casual 1"),
            ("uBody_Irci5F_c000", "Female Elusia Casual 2"), ("uBody_Irci6F_c000", "Female Elusia Casual 3"),
            ("uBody_Solu1F_c000", "Female Solm Formal 1"), ("uBody_Solu2F_c000", "Female Solm Formal 2"),
            ("uBody_Solu3F_c000", "Female Solm Formal 3"), ("uBody_Solu4F_c000", "Female Solm Casual 1"),
            ("uBody_Solu5F_c000", "Female Solm Casual 2"), ("uBody_Solu6F_c000", "Female Solm Casual 3"),
            ("uBody_Lith1F_c000", "Female Lythos 1"), ("uBody_Lith2F_c000", "Female Lythos 2"),
            ("uBody_Lith3F_c000", "Female Lythos 3"), ("uBody_SwimF1_c000", "Female Swimwear 1A"),
            ("uBody_SwimF1_c000b", "Female Swimwear 1B"), ("uBody_SwimF1_c000c", "Female Swimwear 1C"),
            ("uBody_SwimF1_c000d", "Female Swimwear 1D"), ("uBody_SwimF2_c000", "Female Swimwear 2A"),
            ("uBody_SwimF2_c000b", "Female Swimwear 2B"), ("uBody_SwimF2_c000c", "Female Swimwear 2C"),
            ("uBody_SwimF2_c000d", "Female Swimwear 2D"), ("uBody_SwimF3_c000", "Female Swimwear 3A"),
            ("uBody_SwimF3_c000b", "Female Swimwear 3B"), ("uBody_SwimF3_c000c", "Female Swimwear 3C"),
            ("uBody_SwimF3_c000d", "Female Swimwear 3D"), ("uBody_ExerF1_c000", "Female Training Wear A"),
            ("uBody_ExerF1_c000c", "Female Training Wear B"), ("uBody_KimnF_c000", "Female Kimono"),
            ("uBody_CstmF_c000", "Female Sommie Costume A"), ("uBody_CstmF_c699", "Female Sommie Costume B"),
            ("uBody_Cel0AF_c000", "Celica Costume"), ("uBody_Mic0AF_c000", "Micaiah Costume"),
            ("uRig_Lyn0AF", "Lyn Costume"), ("uBody_Luc0AF_c000", "Lucina Costume"),
            ("uBody_Cor0AF_c000", "Corrin Costume"), ("uBody_Eir0AF_c000", "Eirika Costume"),
        };

        internal List<(string id, string name)> AllDressModels { get; } = new();
        #endregion
        #region Emblem IDs
        internal List<(string id, string name)> AlearEmblems { get; } = new()
        {
            ("GID_リュール", "Alear")
        };

        internal List<(string id, string name)> LinkableEmblems { get; } = new()
        {
            ("GID_マルス", "Marth"), ("GID_シグルド", "Sigurd"), ("GID_セリカ", "Celica"), ("GID_ミカヤ", "Micaiah"),
            ("GID_ロイ", "Roy"), ("GID_リーフ", "Leif"), ("GID_ルキナ", "Lucina"), ("GID_リン", "Lyn"),
            ("GID_アイク", "Ike"), ("GID_ベレト", "Byleth"), ("GID_カムイ", "Corrin"), ("GID_エイリーク", "Eirika"),
            ("GID_エーデルガルト", "Edelgard"), ("GID_チキ", "Tiki"), ("GID_ヘクトル", "Hector"), ("GID_ヴェロニカ", "Veronica"),
            ("GID_セネリオ", "Soren"), ("GID_カミラ", "Camilla"), ("GID_クロム", "Chrom")
        };

        internal List<(string id, string name)> AllyEngageableEmblems { get; } = new(); // AlearEmblem + LinkableEmblems

        internal List<(string id, string name)> AllySynchableEmblems { get; } = new() // AllyEngageableEmblems +
        {
            ("GID_エフラム", "Ephraim"), ("GID_ディミトリ", "Dimitri"), ("GID_クロード", "Claude")
        };

        internal List<(string id, string name)> EnemyEngageableEmblems { get; } = new()
        {
            ("GID_M002_シグルド", "Sigurd (Chapter 2)"), ("GID_M007_敵ルキナ", "Corrupted Lucina"),
            ("GID_M007_敵ルキナ", "Corrupted Lucina"), ("GID_M008_敵リーフ", "Corrupted Leif (Chapter 8)"),
            ("GID_M010_敵ベレト", "Corrupted Byleth (Chapter 10)"), ("GID_M010_敵リン", "Corrupted Lyn"),
            ("GID_M011_敵マルス", "Corrupted Marth (Chapter 11)"), ("GID_M011_敵シグルド", "Corrupted Sigurd (Chapter 11)"),
            ("GID_M011_敵セリカ", "Corrupted Celica (Chapter 11)"), ("GID_M011_敵ミカヤ", "Corrupted Micaiah (Chapter 11)"),
            ("GID_M011_敵ロイ", "Corrupted Roy (Chapter 11)"), ("GID_M011_敵リーフ", "Corrupted Leif (Chapter 11)"),
            ("GID_M014_敵ベレト", "Corrupted Byleth (Chapter 14)"), ("GID_M017_敵マルス", "Corrupted Marth (Chapter 17)"),
            ("GID_M017_敵シグルド", "Corrupted Sigurd (Chapter 17)"), ("GID_M017_敵セリカ", "Corrupted Celica (Chapter 17)"),
            ("GID_M017_敵ミカヤ", "Corrupted Micaiah (Chapter 17)"), ("GID_M017_敵ロイ", "Corrupted Roy (Chapter 17)"),
            ("GID_M017_敵リーフ", "Corrupter Leif (Chapter 17)"), ("GID_M019_敵ミカヤ", "Corrupted Micaiah (Chapter 19)"),
            ("GID_M019_敵ロイ", "Corrupted Roy (Chapter 19)"), ("GID_M020_敵セリカ", "Corrupted Celica (Chapter 20)"),
            ("GID_M021_敵マルス", "Corrupted Marth (Chapter 21)"), ("GID_M024_敵マルス", "Corrupted Marth (Chapter 24)"),
            ("GID_M026_敵メディウス", "Medeus"), ("GID_M026_敵ロプトウス", "Loptous"),
            ("GID_M026_敵ドーマ", "Duma"), ("GID_M026_敵ベルド", "Veld"),
            ("GID_M026_敵イドゥン", "Idunn"), ("GID_M026_敵ネルガル", "Nergal"),
            ("GID_M026_敵フォデス", "Fomortiis"), ("GID_M026_敵アシュナード", "Ashnard"),
            ("GID_M026_敵アスタルテ", "Ashera"), ("GID_M026_敵ギムレー", "Grima"),
            ("GID_M026_敵ハイドラ", "Anankos"), ("GID_M026_敵ネメシス", "Nemesis"),
            ("GID_E001_敵チキ", "Corrupted Tiki (Xenologue 1)"), ("GID_E002_敵ヘクトル", "Corrupted Hector (Xenologue 2)"),
            ("GID_E003_敵ヴェロニカ", "Corrupted Veronica (Xenologue 3)"), ("GID_E004_敵セネリオ", "Corrupted Soren (Xenologue 4)"),
            ("GID_E004_敵カミラ", "Corrupted Camilla (Xenologue 4)"), ("GID_E005_敵クロム", "Corrupted Chrom (Xenologue 5)"),
            ("GID_E005_敵ヘクトル", "Corrupted Hector (Xenologue 5)"), ("GID_E005_敵ヴェロニカ", "Corrupted Veronica (Xenologue 5)"),
            ("GID_E006_敵チキ", "Corrupted Tiki (Xenologue 6)"), ("GID_E006_敵ヘクトル", "Corrupted Hector (Xenologue 6)"),
            ("GID_E006_敵ヴェロニカ", "Corrupted Veronica (Xenologue 6)"), ("GID_E006_敵セネリオ", "Corrupted Soren (Xenologue 6)"),
            ("GID_E006_敵カミラ", "Corrupted Camilla (Xenologue 6)"), ("GID_E006_敵クロム", "Corrupted Chrom (Xenologue 6)"),
            ("GID_E006_敵エーデルガルト", "Corrupted Edelgard")
        };

        internal List<(string id, string name)> EnemySynchableEmblems { get; } = new() // EnemyEngageableEmblems + 
        {
            ("GID_E006_敵ディミトリ", "Corrupted Dimitri"), ("GID_E006_敵クロード", "Corrupted Claude")
        };

        internal List<(string id, string name)> EngageableEmblems { get; } = new(); // AllyEngageableEmblems + EnemyEngageableEmblems

        internal List<(string id, string name)> BaseArenaEmblems { get; } = new()
        {
            ("GID_相手マルス", "Marth (Arena)"), ("GID_相手シグルド", "Sigurd (Arena)"),
            ("GID_相手セリカ", "Celica (Arena)"), ("GID_相手ミカヤ", "Micaiah (Arena)"),
            ("GID_相手ロイ", "Roy (Arena)"), ("GID_相手リーフ", "Leif (Arena)"),
            ("GID_相手ルキナ", "Lucina (Arena)"), ("GID_相手リン", "Lyn (Arena)"),
            ("GID_相手アイク", "Ike (Arena)"), ("GID_相手ベレト", "Byleth (Arena)"),
            ("GID_相手カムイ", "Corrin (Arena)"), ("GID_相手エイリーク", "Eirika (Arena)"),
            ("GID_相手リュール", "Alear (Arena)"),
            ("GID_相手エーデルガルト", "Edelgard (Arena)"), ("GID_相手チキ", "Tiki (Arena)"),
            ("GID_相手ヘクトル", "Hector (Arena)"), ("GID_相手ヴェロニカ", "Veronica (Arena)"),
            ("GID_相手セネリオ", "Soren (Arena)"), ("GID_相手カミラ", "Camilla (Arena)"),
            ("GID_相手クロム", "Chrom (Arena)")
        };

        internal List<(string id, string name)> ArenaEmblems { get; } = new() // BaseArenaEmblems +
        {
            ("GID_相手エフラム", "Ephraim (Arena)"), ("GID_相手ディミトリ", "Dimitri (Arena)"),
            ("GID_相手クロード", "Claude (Arena)")
        };

        internal List<(string id, string name)> AllyArenaSynchableEmblems { get; } = new(); // AllySynchableEmblems + ArenaEmblems

        internal List<(string id, string name)> SynchableEmblems { get; } = new(); // AllyArenaSynchableEmblems + EnemySynchableEmblems

        internal List<(string id, string name)> Emblems { get; } = new() // SynchableEmblems +
        {
            ("GID_M000_マルス", "Marth (Prologue)"), ("GID_ルフレ", "Robin")
        };
        #endregion
        #region InfoAnim IDs
        internal List<(string id, string name)> UniqueMaleInfoAnims { get; } = new()
        {
            ("AOC_Info_c000", "Default Male"), 
            ("AOC_Info_c001", "Male Alear"), ("AOC_Info_c001_Eng", "Male Alear Engaged"),
            ("AOC_Info_c049", "Rafal A"), ("AOC_Info_c049_Eng", "Rafal Engaged"),
            ("AOC_Info_c049b", "Rafal B"), ("AOC_Info_c049c", "Rafal C"),
            ("AOC_Info_c100", "Alfred A"), ("AOC_Info_c100_Eng", "Alfred Engaged"),
            ("AOC_Info_c100b", "Alfred B"), ("AOC_Info_c101", "Boucheron A"),
            ("AOC_Info_c101b", "Boucheron B"), ("AOC_Info_c101_Eng", "Boucheron Engaged"),
            ("AOC_Info_c102", "Louis"), ("AOC_Info_c102_Eng", "Louis Engaged"),
            ("AOC_Info_c103", "Jean"), ("AOC_Info_c103_Eng", "Jean Engaged"),
            ("AOC_Info_c200", "Diamant A"),
            ("AOC_Info_c200_Eng", "Diamant Engaged"), ("AOC_Info_c200b", "Diamant B"),
            ("AOC_Info_c201", "Alcryst A"), ("AOC_Info_c201b", "Alcryst B"),
            ("AOC_Info_c201_Eng", "Alcryst Engaged"), ("AOC_Info_c201c", "Alcryst C"),
            ("AOC_Info_c202", "Corrupted Morion"), ("AOC_Info_c203", "Amber"),
            ("AOC_Info_c203_Eng", "Amber Engaged"), 
            ("AOC_Info_c300", "Hyacinth"), ("AOC_Info_c301", "Zelkov"),
            ("AOC_Info_c301_Eng", "Zelkov Engaged"), ("AOC_Info_c302", "Kagetsu"),
            ("AOC_Info_c302_Eng", "Kagetsu Engaged"), ("AOC_Info_c304", "Lindon"),
            ("AOC_Info_c304_Eng", "Lindon Engaged"), 
            ("AOC_Info_c400", "Fogado A"), ("AOC_Info_c400_Eng", "Fogado Engaged"),
            ("AOC_Info_c400b", "Fogado B"), ("AOC_Info_c401", "Pandreo"),
            ("AOC_Info_c401_Eng", "Pandreo"), ("AOC_Info_c402", "Bunet"),
            ("AOC_Info_c402_Eng", "Bunet Engaged"), ("AOC_Info_c403", "Seadall A"),
            ("AOC_Info_c403b", "Seadall B"), ("AOC_Info_c403_Eng", "Seadall Engaged"),
            ("AOC_Info_c500", "Vander"),
            ("AOC_Info_c500_Eng", "Vander Engaged"), ("AOC_Info_c501", "Clanne"),
            ("AOC_Info_c501_Eng", "Clanne Engaged"), ("AOC_Info_c502", "Mauvier"),
            ("AOC_Info_c502_Eng", "Mauvier Engaged"), ("AOC_Info_c503", "Griss"),
            ("AOC_Info_c503b", "Gregory"), ("AOC_Info_c503b_Eng", "Gregory Engaged"),
            ("AOC_Info_c811", "Rodine"),
            ("AOC_Info_c812", "Nelucce"), ("AOC_Info_c815", "Teronda"),
            ("AOC_Info_c813", "Tetchie"), ("AOC_Info_c814", "Totchie"),
            ("AOC_Info_c530", "Marth"),
            ("AOC_Info_c531", "Sigurd"), ("AOC_Info_c532", "Leif"),
            ("AOC_Info_c533", "Roy"), ("AOC_Info_c534", "Ike"),
            ("AOC_Info_c535", "Byleth"), ("AOC_Info_c536", "Ephraim"),
            ("AOC_Info_c514", "Dimitri"), ("AOC_Info_c515", "Claude"),
            ("AOC_Info_c510", "Hector"), ("AOC_Info_c511", "Soren"),
            ("AOC_Info_c512", "Chrom"), ("AOC_Info_c513", "Robin"),
        };

        internal List<(string id, string name)> GenericMaleInfoAnims { get; } = new()
        {
            ("AOC_Info_c702", "Default Corrupted Male"),
            ("AOC_Info_c604", "Male Sword Wielder"), ("AOC_Info_c720", "Corrupted Male Sword Wielder"),
            ("AOC_Info_c610", "Male Lance Wielder"), ("AOC_Info_c722", "Corrupted Male Lance Wielder"),
            ("AOC_Info_c617", "Male Axe Wielder"), ("AOC_Info_c724", "Corrupted Male Axe Wielder"),
            ("AOC_Info_c630", "Male Armored"), ("AOC_Info_c728", "Corrupted Male Armored"),
            ("AOC_Info_c623", "Male Bow Wielder"), ("AOC_Info_c726", "Corrupted Male Bow Wielder"),
            ("AOC_Info_c637", "Male Cavalry"), ("AOC_Info_c730", "Corrupted Male Cavalry"),
            ("AOC_Info_c652", "Male Griffin/Wyvern Unit"), ("AOC_Info_c733", "Corrupted Male Griffin/Wyvern Unit"),
            ("AOC_Info_c657", "Male Dagger Wielder"), ("AOC_Info_c735", "Corrupted Male Dagger Wielder"),
            ("AOC_Info_c659", "Male Tome Wielder"), ("AOC_Info_c737", "Corrupted Male Tome Wielder"),
            ("AOC_Info_c666", "Male Arts Wielder"), ("AOC_Info_c739", "Corrupted Male Arts Wielder"),
            ("AOC_Info_c681", "Male Barbarian"), ("AOC_Info_c697", "Male Enchanter"),
            ("AOC_Info_c695", "Male Mage Cannoneer"),
        };

        internal List<(string id, string name)> UniqueFemaleInfoAnims { get; } = new()
        {
            ("AOC_Info_c050", "Default Female"),
            ("AOC_Info_c051", "Female Alear"), ("AOC_Info_c051_Eng", "Female Alear Engaged"),
            ("AOC_Info_c099", "Nel"), ("AOC_Info_c099_Eng", "Nel Engaged"),
            ("AOC_Info_c150", "Céline A"), ("AOC_Info_c150_Eng", "Céline Engaged"),
            ("AOC_Info_c150b", "Céline B"), ("AOC_Info_c152", "Etie"),
            ("AOC_Info_c152_Eng", "Etie Engaged"), ("AOC_Info_c153", "Chloé"),
            ("AOC_Info_c153_Eng", "Chloé Engaged"), ("AOC_Info_c250", "Jade"),
            ("AOC_Info_c250_Eng", "Jade Engaged"), ("AOC_Info_c251", "Lapis A"),
            ("AOC_Info_c251b", "Lapis B"), ("AOC_Info_c251_Eng", "Lapis Engaged"),
            ("AOC_Info_c252", "Citrinne"), ("AOC_Info_c252_Eng", "Citrinne Engaged"),
            ("AOC_Info_c253", "Yunaka"), ("AOC_Info_c253_Eng", "Yunaka Engaged"),
            ("AOC_Info_c254", "Saphir"), ("AOC_Info_c254_Eng", "Saphir Engaged"),
            ("AOC_Info_c303", "Rosado"), ("AOC_Info_c303_Eng", "Rosado Engaged"),
            ("AOC_Info_c350", "Ivy A"),
            ("AOC_Info_c350b", "Ivy B"), ("AOC_Info_c350_Eng", "Ivy Engaged"),
            ("AOC_Info_c350c", "Ivy C"), ("AOC_Info_c351", "Hortensia A"),
            ("AOC_Info_c351_Eng", "Hortensia Engaged"), ("AOC_Info_c351b", "Hortensia B"),
            ("AOC_Info_c352", "Goldmary"), ("AOC_Info_c352_Eng", "Goldmary Engaged"),
            ("AOC_Info_c450", "Timerra A"), ("AOC_Info_c450_Eng", "Timerra Engaged"),
            ("AOC_Info_c450b", "Timerra B"), ("AOC_Info_c452", "Merrin"),
            ("AOC_Info_c452_Eng", "Merrin Engaged"), ("AOC_Info_c453", "Panette"),
            ("AOC_Info_c453_Eng", "Panette Engaged"),
            ("AOC_Info_c550", "Framme"), ("AOC_Info_c550_Eng", "Framme Engaged"),
            ("AOC_Info_c551", "Veyle A"), ("AOC_Info_c551b", "Veyle B"),
            ("AOC_Info_c551_Eng", "Veyle Engaged"), ("AOC_Info_c552", "Anna"),
            ("AOC_Info_c552_Eng", "Anna Engaged"), ("AOC_Info_c553", "Zephia"),
            ("AOC_Info_c553b", "Zelestia A"), ("AOC_Info_c553c", "Zelestia B"),
            ("AOC_Info_c553b_Eng", "Zelestia Engaged"), ("AOC_Info_c554", "Marni"),
            ("AOC_Info_c554b", "Madeline"), ("AOC_Info_c554b_Eng", "Madeline Engaged"),
            ("AOC_Info_c555", "Lumera"), ("AOC_Info_c558", "Corrupted Lumera"),
            ("AOC_Info_c855", "Abyme"), ("AOC_Info_c859", "Mitan"),
            ("AOC_Info_c580", "Celica"), ("AOC_Info_c581", "Lyn"),
            ("AOC_Info_c582", "Eirika"), ("AOC_Info_c583", "Micaiah"),
            ("AOC_Info_c584", "Lucina"), ("AOC_Info_c585", "Corrin"),
            ("AOC_Info_c560", "Tiki"), ("AOC_Info_c563", "Edelgard"),
            ("AOC_Info_c561", "Camilla"), ("AOC_Info_c562", "Veronica"),
        };

        internal List<(string id, string name)> GenericFemaleInfoAnims { get; } = new()
        {
            ("AOC_Info_c703", "Default Corrupted Female"),
            ("AOC_Info_c605", "Female Sword Wielder"), ("AOC_Info_c721", "Corrupted Female Sword Wielder"),
            ("AOC_Info_c611", "Female Lance Wielder"), ("AOC_Info_c723", "Corrupted Female Lance Wielder"),
            ("AOC_Info_c618", "Female Axe Wielder"), ("AOC_Info_c725", "Corrupted Female Axe Wielder"),
            ("AOC_Info_c631", "Female Armored"), ("AOC_Info_c729", "Corrupted Female Armored"),
            ("AOC_Info_c624", "Female Bow Wielder"), ("AOC_Info_c727", "Corrupted Female Bow Wielder"),
            ("AOC_Info_c638", "Female Cavalry"), ("AOC_Info_c731", "Corrupted Female Cavalry"),
            ("AOC_Info_c646", "Flier"), ("AOC_Info_c732", "Corrupted Flier"),
            ("AOC_Info_c653", "Female Griffin/Wyvern Unit"), ("AOC_Info_c734", "Corrupted Female Griffin/Wyvern Unit"),
            ("AOC_Info_c658", "Female Dagger Wielder"), ("AOC_Info_c736", "Corrupted Female Dagger Wielder"),
            ("AOC_Info_c660", "Female Tome Wielder"), ("AOC_Info_c738", "Corrupted Female Tome Wielder"),
            ("AOC_Info_c667", "Female Arts Wielder"), ("AOC_Info_c740", "Corrupted Female Arts Wielder"),
            ("AOC_Info_c682", "Female Barbarian"), ("AOC_Info_c698", "Female Enchanter"),
            ("AOC_Info_c696", "Female Mage Cannoneer"),
        };
        #endregion
        #region Item IDs
        internal List<(string id, string name)> EngageWeapons { get; } = new()
        {
            ("IID_マルス_レイピア", "Rapier (Marth)"), ("IID_マルス_メリクルソード", "Mercurius"), ("IID_マルス_ファルシオン", "Falchion (Marth)"), ("IID_シグルド_ナイトキラー", "Ridersbane"),
            ("IID_シグルド_ゆうしゃのやり", "Brave Lance"), ("IID_シグルド_ティルフィング", "Tyrfing"), ("IID_セリカ_エンジェル", "Seraphim"), ("IID_セリカ_リカバー", "Recover"),
            ("IID_セリカ_ライナロック", "Ragnarok"), ("IID_ミカヤ_シャイン", "Shine"), ("IID_ミカヤ_リザイア", "Nosferatu"), ("IID_ミカヤ_セイニー", "Thani"),
            ("IID_ロイ_ランスバスター", "Lancereaver"), ("IID_ロイ_ドラゴンキラー", "Wyrmslayer"), ("IID_ロイ_封印の剣", "Binding Blade"), ("IID_リーフ_キラーアクス", "Killer Axe"),
            ("IID_リーフ_キラーアクス_M008", "Killer Axe (Chapter 8)"), ("IID_リーフ_キラーアクス_M011", "Killer Axe (Chapter 11)"), ("IID_リーフ_マスターランス", "Master Lance"), ("IID_リーフ_マスターランス_M008", "Master Lance (Chapter 8)"),
            ("IID_リーフ_ひかりの剣", "Light Brand"), ("IID_リーフ_マスターボウ", "Master Bow"), ("IID_ルキナ_ノーブルレイピア", "Noble Rapier"), ("IID_ルキナ_ノーブルレイピア_M007", "Noble Rapier (Chapter 7)"),
            ("IID_ルキナ_パルティア", "Parthia"), ("IID_ルキナ_裏剣ファルシオン", "Parallel Falchion"), ("IID_リン_キラーボウ", "Killer Bow"), ("IID_リン_キラーボウ_M010", "Killer Bow (Chapter 10)"),
            ("IID_リン_マーニ・カティ", "Mani Katti"), ("IID_リン_マーニ・カティ_M010", "Mani Katti (Chapter 10)"), ("IID_リン_ミュルグレ", "Mulagir"), ("IID_アイク_ハンマー", "Hammer"),
            ("IID_アイク_ウルヴァン", "Urvan"), ("IID_アイク_ラグネル", "Ragnell"), ("IID_ベレト_アイムール", "Aymr (Byleth)"), ("IID_ベレト_ブルトガング", "Blutgang"),
            ("IID_ベレト_ルーン", "Lúin"), ("IID_ベレト_ルーン_M010", "Lúin (Chapter 10)"), ("IID_ベレト_アラドヴァル", "Areadbhar (Byleth)"), ("IID_ベレト_アイギスの盾", "Aegis Shield"),
            ("IID_ベレト_フェイルノート", "Failnaught"), ("IID_ベレト_テュルソスの杖", "Thyrsus"), ("IID_ベレト_ラファイルの宝珠", "Rafail Gem"), ("IID_ベレト_ヴァジュラ", "Vajra-Mushti"),
            ("IID_ベレト_天帝の覇剣", "Sword of the Creator"), ("IID_ベレト_天帝の覇剣_M014", "Sword of the Creator (Chapter 14)"), ("IID_カムイ_逆刀", "Dual Katana"), ("IID_カムイ_飛刀", "Wakizashi"),
            ("IID_カムイ_夜刀神", "Yato"), ("IID_エイリーク_レイピア", "Rapier (Eirika)"), ("IID_エイリーク_かぜの剣", "Wind Sword"), ("IID_エイリーク_ジークリンデ", "Sieglinde"),
            ("IID_エフラム_ジークムント", "Siegmund"), ("IID_リュール_オリゴルディア", "Oligoludia"), ("IID_リュール_竜神の法", "Dragon's Fist"), ("IID_リュール_ライラシオン", "Lyrátion"),
            ("IID_三級長_アイムール", "Aymr (Edelgard)"), ("IID_三級長_アラドヴァル", "Areadbhar (Dimitri)"), ("IID_三級長_フェイルノート", "Failnaught"), ("IID_チキ_つめ", "Eternal Claw"),
            ("IID_チキ_つめ_E006", "Eternal Claw (Xenologue 6)"), ("IID_チキ_しっぽ", "Tail Smash"), ("IID_チキ_しっぽ_E006", "Tail Smash (Xenologue 6)"), ("IID_チキ_ブレス", "Fire Breath"),
            ("IID_チキ_ブレス_竜族", "Fog Breath"), ("IID_チキ_ブレス_重装", "Ice Breath"), ("IID_チキ_ブレス_飛行", "Flame Breath"), ("IID_チキ_ブレス_魔法", "Dark Breath"),
            ("IID_チキ_ブレス_E001", "Fire Breath (Xenologue 1)"), ("IID_チキ_ブレス_E006", "Fire Breath (Xenologue 6)"), ("IID_ヘクトル_ヴォルフバイル", "Wolf Beil"), ("IID_ヘクトル_ルーンソード", "Runesword"),
            ("IID_ヘクトル_ルーンソード_闇", "Corrupted Runesword"), ("IID_ヘクトル_アルマーズ", "Armads"), ("IID_ヴェロニカ_フリズスキャルヴ", "Hliðskjálf"), ("IID_ヴェロニカ_リザーブ＋", "Fortify+"),
            ("IID_ヴェロニカ_エリヴァーガル", "Élivágar"), ("IID_セネリオ_サンダーストーム", "Bolting"), ("IID_セネリオ_サンダーストーム_闇", "Corrupted Bolting"), ("IID_セネリオ_マジックシールド", "Reflect"),
            ("IID_セネリオ_レクスカリバー", "Rexcalibur"), ("IID_カミラ_ボルトアクス", "Bolt Axe"), ("IID_カミラ_ライトニング", "Lightning"), ("IID_カミラ_カミラの艶斧", "Camilla's Axe"),
            ("IID_クロム_サンダーソード", "Levin Sword"), ("IID_クロム_トロン", "Thoron"), ("IID_クロム_神剣ファルシオン", "Falchion (Chrom)")
        };
        #endregion
        #region Ride Dress Model IDs
        internal List<(string id, string name)> HorseRideDressModels { get; } = new()
        {
            ("uBody_Lnc2BR_c000", "Royal Knight Horse"), ("uBody_Lnc2BR_c707", "Corrupted Royal Knight Horse"),
            ("uBody_Amr2BR_c000", "Great Knight Horse"), ("uBody_Amr2BR_c707", "Corrupted Great Knight Horse"),
            ("uBody_Bow2BR_c000", "Bow Knight Horse"), ("uBody_Bow2BR_c707", "Corrupted Bow Knight Horse"),
            ("uBody_Cav0BR_c000", "Sword/Lance/Axe Cavalier Horse"), ("uBody_Cav0BR_c707", "Corrupted Sword/Lance/Axe Cavalier Horse"),
            ("uBody_Cav1BR_c000", "Paladin Horse"), ("uBody_Cav1BR_c707", "Corrupted Paladin Horse"),
            ("uBody_Mag2BR_c000", "Mage Knight Horse"), ("uBody_Mag2BR_c707", "Corrupted Mage Knight Horse"),
            ("uBody_Avn0BR_c100", "Avenir Horse"), ("uBody_Cpd0BR_c400", "Cupido Horse"),
            ("uBody_Sig0BR_c531", "Sigurd's Horse"), ("uBody_Sig0BR_c538", "Corrupted Sigurd's Horse"),
        };
        internal List<(string id, string name)> PegasusRideDressModels { get; } = new()
        {
            ("uBody_Wng0ER_c000", "Sword/Lance/Axe Flier Pegasus"), ("uBody_Wng0ER_c707", "Corrupted Sword/Lance/Axe Flier Pegasus"),
            ("uBody_Slp0ER_c351", "Sleipnir Rider Pegasus"),
        };
        internal List<(string id, string name)> WolfRideDressModels { get; } = new()
        {
            ("uBody_Cav2CR_c000", "Wolf Knight Wolf"), ("uBody_Cav2CR_c707", "Corrupted Wolf Knight Wolf"),
            ("uBody_Wlf0CT_c707", "Corrupted Wolf"), ("uBody_Wlf0CT_c715", "Phantom Wolf"),
            ("uBody_Wlf0CT_c751", "Rare Corrupted Wolf"), ("uBody_Cav2CR_c452", "Wolf Knight (Merrin) Wolf"),
        };
        internal List<(string id, string name)> WyvernRideDressModels { get; } = new()
        {
            ("uBody_Wng2DR_c000", "Wyvern Knight Wyvern"), ("uBody_Wng2DR_c707", "Corrupted Wyvern Knight Wyvern"),
            ("uBody_Wng2DR_c303", "Wyvern Knight (Rosado) Wyvern"), ("uBody_Lnd0DR_c350", "Lindwurm Wyvern"),
            ("uBody_Msn0DR_c553", "Melusine (Zephia) Wyvern"), ("uBody_Msn0DR_c553b", "Melusine (Zelestia) Wyvern"),
            ("uBody_Cmi0DR_c561", "Camilla's Wyvern"), ("uBody_Cmi0DR_c568", "Corrupted Camilla's Wyvern"),
        };
        #endregion
        #region Skill IDs

        internal List<(string id, string name)> TriggerAttackSkills { get; } = new()
        {
            ("SID_マルスエンゲージ技", "Lodestar Rush"), ("SID_マルスエンゲージ技_竜族", "Lodestar Rush [Dragon]"),
            ("SID_マルスエンゲージ技_連携", "Lodestar Rush [Backup]"), ("SID_マルスエンゲージ技_魔法", "Lodestar Rush [Mystical]"),
            ("SID_踏み込み", "Advance"),
            ("SID_ロイエンゲージ技", "Blazing Lion"), ("SID_ロイエンゲージ技_竜族", "Blazing Lion [Dragon]"),
            ("SID_ロイエンゲージ技_魔法", "Blazing Lion [Mystical]"), ("SID_リーフエンゲージ技", "Quadruple Hit"),
            ("SID_リーフエンゲージ技_竜族", "Quadruple Hit [Dragon]"), ("SID_リーフエンゲージ技_隠密", "Quadruple Hit [Covert]"),
            ("SID_リーフエンゲージ技_気功", "Quadruple Hit [Qi Adept]"),
            ("SID_リンエンゲージ技", "Astra Storm"), ("SID_リンエンゲージ技_竜族", "Astra Storm [Dragon]"),
            ("SID_リンエンゲージ技_隠密", "Astra Storm [Covert]"), ("SID_リンエンゲージ技_気功", "Astra Storm [Qi Adept]"),
            ("SID_リンエンゲージ技_威力減", "Weak Astra Storm"), ("SID_リンエンゲージ技_闇_気功", "Weak Astra Storm [Qi Adept]"),
            ("SID_カムイエンゲージ技", "Torrential Roar"), ("SID_カムイエンゲージ技_竜族", "Torrential Roar [Dragon]"),
            ("SID_リュールエンゲージ技", "Dragon Blast"),
            ("SID_リュールエンゲージ技_竜族", "Dragon Blast [Dragon]"), ("SID_リュールエンゲージ技_連携", "Dragon Blast [Backup]"),
            ("SID_リュールエンゲージ技_魔法", "Dragon Blast [Mystical]"), ("SID_リュールエンゲージ技_気功", "Dragon Blast [Qi Adept]"),
            ("SID_切り抜け", "Run Through"),
            ("SID_幻月", "Paraselene"), ("SID_計略_引込の計", "Assembly Gambit"),
            ("SID_計略_猛火計", "Flame Gambit"), ("SID_計略_聖盾の備え", "Shield Gambit"),
            ("SID_計略_毒矢", "Poison Gambit"), ("SID_戦技_狂嵐", "Raging Storm"),
            ("SID_戦技_狂嵐_竜族", "Raging Storm [Dragon]"), ("SID_戦技_狂嵐_隠密", "Raging Storm [Covert]"),
            ("SID_戦技_無残", "Atrocity"), ("SID_戦技_無残_竜族", "Atrocity [Dragon]"),
            ("SID_戦技_無残_隠密", "Atrocity [Covert]"), ("SID_戦技_落星", "Fallen Star"),
            ("SID_戦技_落星_竜族", "Fallen Star [Dragon]"), ("SID_戦技_落星_隠密", "Fallen Star [Covert]"),
            ("SID_三級長エンゲージ技", "Houses Unite"), ("SID_三級長エンゲージ技_竜族", "Houses Unite [Dragon]"),
            ("SID_三級長エンゲージ技_騎馬", "Houses Unite [Cavalry]"), ("SID_三級長エンゲージ技_隠密", "Houses Unite [Covert]"),
            ("SID_三級長エンゲージ技_重装", "Houses Unite [Armored]"), ("SID_三級長エンゲージ技_気功", "Houses Unite [Qi Adept]"),
            ("SID_三級長エンゲージ技＋", "Houses Unite+"), ("SID_三級長エンゲージ技＋_竜族", "Houses Unite+ [Dragon]"),
            ("SID_三級長エンゲージ技＋_騎馬", "Houses Unite+ [Cavalry]"), ("SID_三級長エンゲージ技＋_隠密", "Houses Unite+ [Covert]"),
            ("SID_三級長エンゲージ技＋_重装", "Houses Unite+ [Armored]"), ("SID_三級長エンゲージ技＋_気功", "Houses Unite+ [Qi Adept]"),
            ("SID_セネリオエンゲージ技", "Cataclysm"), ("SID_セネリオエンゲージ技_竜族", "Cataclysm [Dragon]"),
            ("SID_セネリオエンゲージ技_魔法", "Cataclysm [Mystical]"), ("SID_セネリオエンゲージ技_気功", "Cataclysm [Qi Adept]"),
            ("SID_セネリオエンゲージ技＋", "Cataclysm+"), ("SID_セネリオエンゲージ技＋_竜族", "Cataclysm+ [Dragon]"),
            ("SID_セネリオエンゲージ技＋_魔法", "Cataclysm+ [Mystical]"), ("SID_セネリオエンゲージ技＋_気功", "Cataclysm+ [Qi Adept]"),
            ("SID_セネリオエンゲージ技_G004", "Cataclysm (Divine Paralogue)"), ("SID_セネリオエンゲージ技_闇", "Corrupted Cataclysm"),
            ("SID_全弾発射", "Let Fly")
        };

        internal List<(string id, string name)> CompatibleAsEngageAttacks { get; } = new() // TriggerAttackSkills +
        {
            ("SID_シグルドエンゲージ技", "Override"), ("SID_シグルドエンゲージ技_竜族", "Override [Dragon]"),
            ("SID_シグルドエンゲージ技_重装", "Override [Armored]"), ("SID_シグルドエンゲージ技_魔法", "Override [Mystical]"),
            ("SID_シグルドエンゲージ技_気功", "Override [Qi Adept]"),
            ("SID_セリカエンゲージ技", "Warp Ragnarok"),
            ("SID_セリカエンゲージ技_竜族", "Warp Ragnarok [Dragon]"), ("SID_セリカエンゲージ技_騎馬", "Warp Ragnarok [Cavalry]"),
            ("SID_セリカエンゲージ技_飛行", "Warp Ragnarok [Flying]"), ("SID_セリカエンゲージ技_魔法", "Warp Ragnarok [Mystical]"),
            ("SID_セリカエンゲージ技_闇", "Dark Warp"), ("SID_セリカエンゲージ技_闇_M020", "Ragnarok Warp"),
            ("SID_ミカヤエンゲージ技", "Great Sacrifice"),
            ("SID_ミカヤエンゲージ技_竜族", "Great Sacrifice [Dragon]"), ("SID_ミカヤエンゲージ技_重装", "Great Sacrifice [Armored]"),
            ("SID_ミカヤエンゲージ技_気功", "Great Sacrifice [Qi Adept]"),
            ("SID_ルキナエンゲージ技", "All for One"), ("SID_ルキナエンゲージ技_竜族", "All for One [Dragon]"),
            ("SID_ルキナエンゲージ技_連携", "All for One [Backup]"),
            ("SID_アイクエンゲージ技", "Great Aether"), ("SID_アイクエンゲージ技_竜族", "Great Aether [Dragon]"),
            ("SID_アイクエンゲージ技_重装", "Great Aether [Armored]"), ("SID_アイクエンゲージ技_飛行", "Great Aether [Flying]"),
            ("SID_ベレトエンゲージ技", "Goddess Dance"), ("SID_ベレトエンゲージ技_竜族", "Goddess Dance [Dragon]"),
            ("SID_ベレトエンゲージ技_連携", "Goddess Dance [Backup]"), ("SID_ベレトエンゲージ技_騎馬", "Goddess Dance [Cavalry]"),
            ("SID_ベレトエンゲージ技_隠密", "Goddess Dance [Covert]"), ("SID_ベレトエンゲージ技_重装", "Goddess Dance [Armored]"),
            ("SID_ベレトエンゲージ技_飛行", "Goddess Dance [Flying]"), ("SID_ベレトエンゲージ技_魔法", "Goddess Dance [Mystical]"),
            ("SID_ベレトエンゲージ技_気功", "Goddess Dance [Qi Adept]"), ("SID_ベレトエンゲージ技_闇", "Diabolical Dance"),
            ("SID_エイリークエンゲージ技", "Twin Strike"), ("SID_エイリークエンゲージ技_竜族", "Twin Strike [Dragon]"),
            ("SID_エイリークエンゲージ技_騎馬", "Twin Strike [Cavalry]"),
            ("SID_リュールエンゲージ技共同", "Bond Blast"), ("SID_リュールエンゲージ技共同_竜族", "Bond Blast [Dragon]"),
            ("SID_リュールエンゲージ技共同_連携", "Bond Blast [Backup]"), ("SID_リュールエンゲージ技共同_魔法", "Bond Blast [Mystical]"),
            ("SID_リュールエンゲージ技共同_気功", "Bond Blast [Qi Adept]"),
            ("SID_チキエンゲージ技", "Divine Blessing"), ("SID_チキエンゲージ技_竜族", "Divine Blessing [Dragon]"),
            ("SID_チキエンゲージ技_気功", "Divine Blessing [Qi Adept]"), ("SID_チキエンゲージ技_E001", "Divine Blessing (Xenologue 1)"),
            ("SID_チキエンゲージ技＋", "Divine Blessing+"),
            ("SID_チキエンゲージ技＋_竜族", "Divine Blessing+ [Dragon]"), ("SID_チキエンゲージ技＋_気功", "Divine Blessing+ [Qi Adept]"),
            ("SID_ヘクトルエンゲージ技", "Storm's Eye"), ("SID_ヘクトルエンゲージ技_竜族", "Storm's Eye [Dragon]"),
            ("SID_ヘクトルエンゲージ技_連携", "Storm's Eye [Backup]"), ("SID_ヘクトルエンゲージ技_隠密", "Storm's Eye [Covert]"),
            ("SID_ヘクトルエンゲージ技＋", "Storm's Eye+"), ("SID_ヘクトルエンゲージ技＋_竜族", "Storm's Eye+ [Dragon]"),
            ("SID_ヘクトルエンゲージ技＋_連携", "Storm's Eye+ [Backup]"), ("SID_ヘクトルエンゲージ技＋_隠密", "Storm's Eye+ [Covert]"),
            ("SID_ヴェロニカエンゲージ技", "Summon Hero"), ("SID_ヴェロニカエンゲージ技_竜族", "Summon Hero [Dragon]"),
            ("SID_ヴェロニカエンゲージ技_連携", "Summon Hero [Backup]"), ("SID_ヴェロニカエンゲージ技_騎馬", "Summon Hero [Cavalry]"),
            ("SID_カミラエンゲージ技", "Dark Inferno"), ("SID_カミラエンゲージ技_竜族", "Dark Inferno [Dragon]"),
            ("SID_カミラエンゲージ技_魔法", "Dark Inferno [Mystical]"), ("SID_カミラエンゲージ技_気功", "Dark Inferno [Qi Adept]"),
            ("SID_カミラエンゲージ技＋", "Dark Inferno+"), ("SID_カミラエンゲージ技＋_竜族", "Dark Inferno+ [Dragon]"),
            ("SID_カミラエンゲージ技＋_魔法", "Dark Inferno+ [Mystical]"), ("SID_カミラエンゲージ技＋_気功", "Dark Inferno+ [Qi Adept]"),
            ("SID_クロムエンゲージ技", "Giga Levin Sword"), ("SID_クロムエンゲージ技_竜族", "Giga Levin Sword [Dragon]"),
            ("SID_クロムエンゲージ技_飛行", "Giga Levin Sword [Flying]"), ("SID_クロムエンゲージ技_魔法", "Giga Levin Sword [Mystical]"),
            ("SID_クロムエンゲージ技＋", "Giga Levin Sword+"), ("SID_クロムエンゲージ技＋_竜族", "Giga Levin Sword+ [Dragon]"),
            ("SID_クロムエンゲージ技＋_飛行", "Giga Levin Sword+ [Flying]"), ("SID_クロムエンゲージ技＋_魔法", "Giga Levin Sword+ [Mystical]")
        };

        internal List<(string id, string name)> GeneralSkills { get; } = new() // TriggerAttackSkills +
        {
            ("SID_ＨＰ＋５_継承用", "HP +5"), ("SID_ＨＰ＋７_継承用", "HP +7"), ("SID_ＨＰ＋１０_継承用", "HP +10"), ("SID_ＨＰ＋１２_継承用", "HP +12"),
            ("SID_ＨＰ＋１５_継承用", "HP +15"), ("SID_力＋１_継承用", "Strength +1"), ("SID_力＋２_継承用", "Strength +2"), ("SID_力＋３_継承用", "Strength +3"),
            ("SID_力＋４_継承用", "Strength +4"), ("SID_力＋５_継承用", "Strength +5"), ("SID_力＋６_継承用", "Strength +6"), ("SID_技＋１_継承用", "Dexterity +1"),
            ("SID_技＋２_継承用", "Dexterity +2"), ("SID_技＋３_継承用", "Dexterity +3"), ("SID_技＋４_継承用", "Dexterity +4"), ("SID_技＋５_継承用", "Dexterity +5"),
            ("SID_速さ＋１_継承用", "Speed +1"), ("SID_速さ＋２_継承用", "Speed +2"), ("SID_速さ＋３_継承用", "Speed +3"), ("SID_速さ＋４_継承用", "Speed +4"),
            ("SID_速さ＋５_継承用", "Speed +5"), ("SID_幸運＋２_継承用", "Luck +2"), ("SID_幸運＋４_継承用", "Luck +4"), ("SID_幸運＋６_継承用", "Luck +6"),
            ("SID_幸運＋８_継承用", "Luck +8"), ("SID_幸運＋１０_継承用", "Luck +10"), ("SID_幸運＋１２_継承用", "Luck +12"), ("SID_守備＋１_継承用", "Defense +1"),
            ("SID_守備＋２_継承用", "Defense +2"), ("SID_守備＋３_継承用", "Defense +3"), ("SID_守備＋４_継承用", "Defense +4"), ("SID_守備＋５_継承用", "Defense +5"),
            ("SID_魔力＋２_継承用", "Magic +2"), ("SID_魔力＋３_継承用", "Magic +3"), ("SID_魔力＋４_継承用", "Magic +4"), ("SID_魔力＋５_継承用", "Magic +5"),
            ("SID_魔防＋２_継承用", "Resistance +2"), ("SID_魔防＋３_継承用", "Resistance +3"), ("SID_魔防＋４_継承用", "Resistance +4"), ("SID_魔防＋５_継承用", "Resistance +5"),
            ("SID_体格＋３_継承用", "Build +3"), ("SID_体格＋４_継承用", "Build +4"), ("SID_体格＋５_継承用", "Build +5"), ("SID_移動＋１_継承用", "Movement +1"),
            ("SID_蛇毒", "Poison Strike"), ("SID_死の吐息", "Savage Blow"), ("SID_剣殺し", "Swordbreaker"), ("SID_槍殺し", "Lancebreaker"),
            ("SID_斧殺し", "Axebreaker"), ("SID_魔殺し", "Tomebreaker"), ("SID_弓殺し", "Bowbreaker"), ("SID_短刀殺し", "Knifebreaker"),
            ("SID_気功殺し", "Artbreaker"), ("SID_力封じ", "Seal Strength"), ("SID_魔力封じ", "Seal Magic"), ("SID_守備封じ", "Seal Defense"),
            ("SID_速さ封じ", "Seal Speed"), ("SID_魔防封じ", "Seal Resistance"), ("SID_鬼神の構え", "Fierce Stance"), ("SID_金剛の構え", "Steady Stance"),
            ("SID_飛燕の構え", "Darting Stance"), ("SID_明鏡の構え", "Warding Stance"), ("SID_死線", "Life and Death"), ("SID_相性激化", "Triangle Adept"),
            ("SID_噛描", "Cornered Beast"), ("SID_自壊", "Self-Destruct"), ("SID_清流の一撃", "Duelist's Blow"), ("SID_飛燕の一撃", "Darting Blow"),
            ("SID_鬼神の一撃", "Death Blow"), ("SID_凶鳥の一撃", "Certain Blow"), ("SID_金剛の一撃", "Armored Blow"), ("SID_明鏡の一撃", "Warding Blow"),
            ("SID_狂乱の一撃", "Spirit Strike"), ("SID_ブレイク無効", "Unbreakable"), ("SID_特効耐性", "Stalwart"), ("SID_特効無効", "Unwavering"),
            ("SID_不動", "Anchor"), ("SID_熟練者", "Veteran"), ("SID_熟練者＋", "Veteran+"), ("SID_虚無の呪い", "Void Curse"),
            ("SID_チェインアタック威力軽減", "Bond Breaker"),
            ("SID_回避＋１０", "Avoid +10"), ("SID_回避＋１５", "Avoid +15"), ("SID_回避＋２０", "Avoid +20"), ("SID_回避＋２５", "Avoid +25"),
            ("SID_回避＋３０", "Avoid +30"), ("SID_命中＋１０", "Hit +10"), ("SID_命中＋１５", "Hit +15"), ("SID_命中＋２０", "Hit +20"),
            ("SID_命中＋２５", "Hit +25"), ("SID_命中＋３０", "Hit +30"), ("SID_必殺回避＋１０", "Dodge +10"), ("SID_必殺回避＋１５", "Dodge +15"),
            ("SID_必殺回避＋２０", "Dodge +20"), ("SID_必殺回避＋２５", "Dodge +25"), ("SID_必殺回避＋３０", "Dodge +30"), ("SID_剣術・柔１", "Sword Agility 1"),
            ("SID_剣術・柔２", "Sword Agility 2"), ("SID_剣術・柔３", "Sword Agility 3"), ("SID_剣術・柔４", "Sword Agility 4"), ("SID_剣術・柔５", "Sword Agility 5"),
            ("SID_剣術・剛１", "Sword Power 1"), ("SID_剣術・剛２", "Sword Power 2"), ("SID_剣術・剛３", "Sword Power 3"), ("SID_剣術・剛４", "Sword Power 4"),
            ("SID_剣術・剛５", "Sword Power 5"), ("SID_剣術・心１", "Sword Focus 1"), ("SID_剣術・心２", "Sword Focus 2"), ("SID_剣術・心３", "Sword Focus 3"),
            ("SID_剣術・心４", "Sword Focus 4"), ("SID_剣術・心５", "Sword Focus 5"), ("SID_槍術・柔１", "Lance Agility 1"), ("SID_槍術・柔２", "Lance Agility 2"),
            ("SID_槍術・柔３", "Lance Agility 3"), ("SID_槍術・柔４", "Lance Agility 4"), ("SID_槍術・柔５", "Lance Agility 5"), ("SID_槍術・剛１", "Lance Power 1"),
            ("SID_槍術・剛２", "Lance Power 2"), ("SID_槍術・剛３", "Lance Power 3"), ("SID_槍術・剛４", "Lance Power 4"), ("SID_槍術・剛５", "Lance Power 5"),
            ("SID_斧術・剛１", "Axe Power 1"), ("SID_斧術・剛２", "Axe Power 2"), ("SID_斧術・剛３", "Axe Power 3"), ("SID_斧術・剛４", "Axe Power 4"),
            ("SID_斧術・剛５", "Axe Power 5"), ("SID_弓術・柔１", "Bow Agility 1"), ("SID_弓術・柔２", "Bow Agility 2"), ("SID_弓術・柔３", "Bow Agility 3"),
            ("SID_弓術・柔４", "Bow Agility 4"), ("SID_弓術・柔５", "Bow Agility 5"), ("SID_弓術・心１", "Bow Focus 1"), ("SID_弓術・心２", "Bow Focus 2"),
            ("SID_弓術・心３", "Bow Focus 3"), ("SID_弓術・心４", "Bow Focus 4"), ("SID_弓術・心５", "Bow Focus 5"), ("SID_体術・心１", "Art Focus 1"),
            ("SID_体術・心２", "Art Focus 2"), ("SID_体術・心３", "Art Focus 3"), ("SID_体術・心４", "Art Focus 4"), ("SID_体術・心５", "Art Focus 5"),
            ("SID_短剣術１", "Knife Precision 1"), ("SID_短剣術２", "Knife Precision 2"), ("SID_短剣術３", "Knife Precision 3"), ("SID_短剣術４", "Knife Precision 4"),
            ("SID_短剣術５", "Knife Precision 5"), ("SID_魔道１", "Tome Precision 1"), ("SID_魔道２", "Tome Precision 2"), ("SID_魔道３", "Tome Precision 3"),
            ("SID_魔道４", "Tome Precision 4"), ("SID_魔道５", "Tome Precision 5"), ("SID_信仰１", "Staff Mastery 1"), ("SID_信仰２", "Staff Mastery 2"),
            ("SID_信仰３", "Staff Mastery 3"), ("SID_信仰４", "Staff Mastery 4"), ("SID_信仰５", "Staff Mastery 5"), ("SID_見切り", "Perceptive"),
            ("SID_見切り＋", "Perceptive+"), ("SID_ブレイク時追撃", "Break Defenses"), ("SID_不屈", "Unyielding"), ("SID_不屈＋", "Unyielding+"),
            ("SID_不屈＋＋", "Unyielding++"), ("SID_カウンター", "Divine Speed"), ("SID_カウンター_隠密", "Divine Speed [Covert]"), ("SID_カウンター_竜族", "Divine Speed [Dragon]"),
            ("SID_再移動", "Canter"), ("SID_再移動＋", "Canter+"), ("SID_助走", "Momentum"), ("SID_助走＋", "Momentum+"),
            ("SID_猛進", "Headlong Rush"), ("SID_迅走", "Gallop"), ("SID_迅走_竜族", "Gallop [Dragon]"), ("SID_迅走_騎馬", "Gallop [Cavalry]"),
            ("SID_迅走_隠密", "Gallop [Covert]"), ("SID_迅走_闇", "Dark Gallop"), ("SID_異形リベンジ", "Holy Stance"), ("SID_異形リベンジ＋", "Holy Stance+"),
            ("SID_異形リベンジ＋＋", "Holy Stance++"), ("SID_異形リベンジ＋＋_闇", "Unholy Stance"), ("SID_共鳴の黒魔法", "Resonance"), ("SID_共鳴の黒魔法＋", "Resonance+"),
            ("SID_大好物", "Favorite Food"), ("SID_重唱", "Echo"), ("SID_重唱_竜族", "Echo [Dragon]"), ("SID_重唱_魔法", "Echo [Mystical]"),
            ("SID_杖使い", "Cleric"), ("SID_杖使い＋", "Cleric+"), ("SID_杖使い＋＋", "Cleric++"), ("SID_サイレス無効", "Silence Ward"),
            ("SID_癒しの響き", "Healing Light"), ("SID_増幅", "Augment"), ("SID_増幅_竜族", "Augment [Dragon]"), ("SID_増幅_気功", "Augment [Qi Adept]"),
            ("SID_増幅_闇", "Dark Augment"), ("SID_踏ん張り", "Hold Out"), ("SID_踏ん張り＋", "Hold Out+"), ("SID_踏ん張り＋＋", "Hold Out++"),
            ("SID_踏ん張り＋＋＋", "Hold Out+++ "), ("SID_超越", "Rise Above"), ("SID_超越_竜族", "Rise Above [Dragon]"), ("SID_超越_騎馬", "Rise Above [Cavalry]"),
            ("SID_超越_重装", "Rise Above [Armored]"), ("SID_超越_闇", "Sink Below"), ("SID_武器相性激化", "Arms Shield"), ("SID_武器相性激化＋", "Arms Shield+"),
            ("SID_武器相性激化＋＋", "Arms Shield++"), ("SID_待ち伏せ", "Vantage"), ("SID_待ち伏せ＋", "Vantage+"), ("SID_待ち伏せ＋＋", "Vantage++"),
            ("SID_順応", "Adaptable"), ("SID_順応_竜族", "Adaptable [Dragon]"), ("SID_順応_連携", "Adaptable [Backup]"), ("SID_順応_隠密", "Adaptable [Covert]"),
            ("SID_順応_重装", "Adaptable [Armored]"), ("SID_順応_飛行", "Adaptable [Flying]"), ("SID_絆の力", "Dual Strike"), ("SID_デュアルアシスト", "Dual Assist"),
            ("SID_デュアルアシスト＋", "Dual Assist+"), ("SID_デュアルサポート", "Dual Support"), ("SID_絆盾", "Bonded Shield"), ("SID_絆盾_竜族", "Bonded Shield [Dragon]"),
            ("SID_絆盾_騎馬", "Bonded Shield [Cavalry]"), ("SID_絆盾_重装", "Bonded Shield [Armored]"), ("SID_絆盾_飛行", "Bonded Shield [Flying]"), ("SID_絆盾_気功", "Bonded Shield [Qi Adept]"),
            ("SID_攻め立て", "Alacrity"), ("SID_攻め立て＋", "Alacrity+"), ("SID_攻め立て＋＋", "Alacrity++"), ("SID_速さの吸収", "Speedtaker"),
            ("SID_残像", "Call Doubles"), ("SID_残像_竜族", "Call Doubles [Dragon]"), ("SID_残像_飛行", "Call Doubles [Flying]"), ("SID_破壊", "Demolish"),
            ("SID_引き戻し", "Reposition"), ("SID_怒り", "Wrath"), ("SID_勇将", "Resolve"), ("SID_勇将＋", "Resolve+"),
            ("SID_アイクエンゲージスキル", "Laguz Friend"), ("SID_アイクエンゲージスキル_竜族", "Laguz Friend [Dragon]"), ("SID_天刻の拍動", "Divine Pulse"), ("SID_天刻の拍動＋", "Divine Pulse+"),
            ("SID_師の導き", "Mentorship"), ("SID_拾得", "Lost & Found "), ("SID_先生", "Instruct"), ("SID_先生_竜族", "Instruct [Dragon]"),
            ("SID_先生_連携", "Instruct [Backup]"), ("SID_先生_騎馬", "Instruct [Cavalry]"), ("SID_先生_隠密", "Instruct [Covert]"), ("SID_先生_重装", "Instruct [Armored]"),
            ("SID_先生_飛行", "Instruct [Flying]"), ("SID_先生_魔法", "Instruct [Mystical]"), ("SID_先生_気功", "Instruct [Qi Adept]"), ("SID_竜脈", "Dragon Vein (Corrin)"),
            ("SID_竜脈_竜族", "Dragon Vein (Corrin) [Dragon]"), ("SID_竜脈_連携", "Dragon Vein (Corrin) [Backup]"), ("SID_竜脈_騎馬", "Dragon Vein (Corrin) [Cavalry]"), ("SID_竜脈_隠密", "Dragon Vein (Corrin) [Covert]"),
            ("SID_竜脈_重装", "Dragon Vein (Corrin) [Armored]"), ("SID_竜脈_飛行", "Dragon Vein (Corrin) [Flying]"), ("SID_竜脈_魔法", "Dragon Vein (Corrin) [Mystical]"), ("SID_竜脈_気功", "Dragon Vein (Corrin) [Qi Adept]"),
            ("SID_竜呪", "Draconic Hex"), ("SID_防陣", "Pair Up"), ("SID_スキンシップ", "Quality Time"), ("SID_スキンシップ＋", "Quality Time+"),
            ("SID_呪縛", "Dreadful Aura"), ("SID_呪縛_隠密", "Dreadful Aura [Covert]"),
            ("SID_月の腕輪", "Lunar Brace"), ("SID_太陽の腕輪", "Solar Brace"), ("SID_日月の腕輪", "Eclipse Brace"), ("SID_月の腕輪＋", "Lunar Brace+"),
            ("SID_太陽の腕輪＋", "Solar Brace+"), ("SID_日月の腕輪＋", "Eclipse Brace+"), ("SID_優風", "Gentility"), ("SID_勇空", "Bravery"),
            ("SID_蒼穹", "Blue Skies"), ("SID_優風＋", "Gentility+"), ("SID_勇空＋", "Bravery+"), ("SID_蒼穹＋", "Blue Skies+"),
            ("SID_リュール邪竜特効", "Holy Aura"), ("SID_神竜の加護", "Holy Shield"), ("SID_絆を繋薙くもの", "Bond Forger"), ("SID_絆を繋薙くもの＋", "Bond Forger+"),
            ("SID_エレオスの祝福", "Boon of Elyos"), ("SID_神竜の結束", "Divinely Inspiring"), ("SID_白の忠誠", "Alabaster Duty"),
            ("SID_碧の信仰", "Verdant Faith"), ("SID_緋い声援", "Crimson Cheer"), ("SID_自己研鑽", "Self-Improver"), ("SID_筋肉増強剤", "Energized"),
            ("SID_涙腺崩壊", "Moved to Tears"), ("SID_平和の花", "Gentle Flower"), ("SID_絵になる二人", "Fairy-Tale Folk"), ("SID_花園の門番", "Admiration"),
            ("SID_真っ向勝負", "Fair Fight"), ("SID_名乗り上げ", "Aspiring Hero"), ("SID_瞑想", "Meditation"), ("SID_僕が守ります！", "Get Behind Me!"),
            ("SID_戦果委譲", "Share Spoils"), ("SID_大盤振る舞い", "Generosity"), ("SID_執着", "Single-Minded"), ("SID_『次』はない", "Not *Quite*"),
            ("SID_光彩奪目！", "Blinding Flash"), ("SID_煌めく理力", "Big Personality"), ("SID_微笑み", "Stunning Smile"), ("SID_溜め息", "Disarming Sigh"),
            ("SID_ソルムの騒音", "Racket of Solm"), ("SID_エスコート", "Knightly Escort"), ("SID_戦の血", "Blood Fury"), ("SID_人たらし", "Charmer"),
            ("SID_大集会", "Party Animal"), ("SID_料理再現", "Seconds?"), ("SID_一攫千金", "Make a Killing"), ("SID_努力の才", "Expertise"),
            ("SID_殺しの技術", "Trained to Kill"), ("SID_神秘の踊り", "Curious Dance"), ("SID_歴戦の勘", "Weapon Insight"), ("SID_勝利への意思", "Will to Win"),
            ("SID_邪竜の救済", "Fell Protection"), ("SID_次への備え", "Contemplative"), ("SID_助太刀", "Brave Assist"), ("SID_挟撃", "Pincer Attack"),
            ("SID_手助け", "Reforge"), ("SID_スマッシュ＋", "Smash+"), ("SID_無慈悲", "Merciless"), ("SID_集中", "No Distractions"),
            ("SID_狙撃", "Careful Aim"), ("SID_入れ替え", "Swap"), ("SID_護衛", "Allied Defense"), ("SID_回り込み", "Pivot"),
            ("SID_足狙い", "Hobble"), ("SID_移動補助", "Clear the Way"), ("SID_急襲", "Air Raid"), ("SID_すり抜け", "Pass"),
            ("SID_魔力増幅", "Spell Harmony"), ("SID_背理の法", "Chaos Style"), ("SID_気の拡散", "Diffuse Healer"), ("SID_自己回復", "Self-Healing"),
            ("SID_神竜気", "Divine Spirit"), ("SID_邪竜気", "Fell Spirit"), ("SID_邪竜気・闇", "Dark Spirit"), ("SID_金蓮", "Golden Lotus"),
            ("SID_華炎", "Ignis"), ("SID_太陽", "Sol"), ("SID_月光", "Luna"), ("SID_虚空", "Grasping Void"),
            ("SID_大樹", "World Tree"), ("SID_砂陣", "Sandstorm"), ("SID_水鏡", "Back at You"), ("SID_魔法剣", "Soulblade"),
            ("SID_特別な踊り", "Special Dance"), ("SID_伝道師", "Sympathetic"), ("SID_必殺剣", "Deadly Blade"), ("SID_百戦練磨", "Battlewise"),
            ("SID_回復", "Renewal"), ("SID_風薙ぎ", "Windsweep"), ("SID_轟雷", "Great Thunder"), ("SID_瞬殺", "Bane"),
            ("SID_慈悲", "Mercy"), ("SID_業火", "Raging Fire"), ("SID_剛腕", "Strong Arm"), ("SID_祈り", "Miracle"),
            ("SID_ダイムサンダ", "Dire Thunder"), ("SID_王の器", "Rightful Ruler"), ("SID_いやしの心", "Healtouch"), ("SID_引き寄せ", "Draw Back"),
            ("SID_ギガスカリバー", "Giga Excalibur"), ("SID_旋風", "Wind Adept"), ("SID_体当たり", "Shove"), ("SID_閃花", "Flickering Flower"),
            ("SID_風神", "Wind God"), ("SID_騎士道", "Chivalry"), ("SID_武士道", "Bushido"), ("SID_滅殺", "Lethality"),
            ("SID_必的", "Sure Strike"), ("SID_絆の指輪_アルフォンス", "Spur Attack"), ("SID_絆の指輪_シャロン", "Fortify Def"), ("SID_絆の指輪_アンナ", "Spur Res"),
            ("SID_武器シンクロ", "Weapon Sync"), ("SID_武器シンクロ＋", "Weapon Sync+"),
            ("SID_血統", "Lineage"), ("SID_戦技", "Combat Arts"), ("SID_戦技_竜族", "Combat Arts [Dragon]"), ("SID_戦技_隠密", "Combat Arts [Covert]"),
            ("SID_光玉の加護", "Lightsphere"), ("SID_星玉の加護", "Starsphere"), ("SID_命玉の加護", "Lifesphere"), ("SID_命玉の加護＋", "Lifesphere+"),
            ("SID_命玉の加護＋＋", "Lifesphere++"), ("SID_地玉の加護", "Geosphere"), ("SID_地玉の加護＋", "Geosphere+"), ("SID_竜化", "Draconic Form"),
            ("SID_竜化_重装", "Draconic Form [Armored]"), ("SID_竜化_魔法", "Draconic Form [Mystical]"), ("SID_切り返し", "Quick Riposte"), ("SID_切り返し＋", "Quick Riposte+"),
            ("SID_重撃", "Heavy Attack"), ("SID_角の睨み", "Piercing Glare"), ("SID_適応能力", "Adaptability"), ("SID_適応能力＋", "Adaptability+"),
            ("SID_鉄壁", "Impenetrable"), ("SID_鉄壁_竜族", "Impenetrable [Dragon]"), ("SID_鉄壁_騎馬", "Impenetrable [Cavalry]"), ("SID_鉄壁_重装", "Impenetrable [Armored]"),
            ("SID_鉄壁_飛行", "Impenetrable [Flying]"), ("SID_SPコンバート", "SP Conversion"), ("SID_血讐", "Reprisal"), ("SID_血讐＋", "Reprisal+"),
            ("SID_限界突破", "Level Boost"), ("SID_異界の力", "Book of Worlds"), ("SID_契約", "Contract"), ("SID_契約_竜族", "Contract [Dragon]"),
            ("SID_契約_連携", "Contract [Backup]"), ("SID_契約_隠密", "Contract [Covert]"), ("SID_理魔法＋", "Anima Focus"), ("SID_慧眼", "Keen Insight"),
            ("SID_慧眼＋", "Keen Insight+"), ("SID_囮指名", "Assign Decoy"), ("SID_復帰阻止", "Block Recovery"), ("SID_陽光", "Flare"),
            ("SID_陽光_竜族", "Flare [Dragon]"), ("SID_陽光_魔法", "Flare [Mystical]"), ("SID_陽光_気功", "Flare [Qi Adept]"), ("SID_陽光_闇", "Corrupted Flare"),
            ("SID_竜脈・異", "Dragon Vein (Camilla)"), ("SID_竜脈・異_竜族", "Dragon Vein (Camilla) [Dragon]"), ("SID_竜脈・異_連携", "Dragon Vein (Camilla) [Backup]"), ("SID_竜脈・異_騎馬", "Dragon Vein (Camilla) [Cavalry]"),
            ("SID_竜脈・異_隠密", "Dragon Vein (Camilla) [Covert]"), ("SID_竜脈・異_重装", "Dragon Vein (Camilla) [Armored]"), ("SID_竜脈・異_飛行", "Dragon Vein (Camilla) [Flying]"), ("SID_竜脈・異_魔法", "Dragon Vein (Camilla) [Mystical]"),
            ("SID_竜脈・異_気功", "Dragon Vein (Camilla) [Qi Adept]"), ("SID_地脈吸収", "Groundswell"), ("SID_デトックス", "Detoxify"), ("SID_後始末", "Decisive Strike"),
            ("SID_後始末＋", "Decisive Strike+"), ("SID_天駆", "Soar"), ("SID_天駆_竜族", "Soar [Dragon]"), ("SID_天駆_騎馬", "Soar [Cavalry]"),
            ("SID_天駆_飛行", "Soar [Flying]"), ("SID_力まかせ", "Brute Force"), ("SID_カリスマ", "Charm"), ("SID_不意打ち", "Surprise Attack"),
            ("SID_七色の叫び", "Rally Spectrum"), ("SID_七色の叫び＋", "Rally Spectrum+"), ("SID_半身_単身用", "Other Half (Arena)"),
            ("SID_力・技＋１", "Str/Dex +1"), ("SID_力・技＋２", "Str/Dex +2"), ("SID_力・技＋３", "Str/Dex +3"), ("SID_力・技＋４", "Str/Dex +4"),
            ("SID_力・技＋５", "Str/Dex +5"), ("SID_ＨＰ・幸運＋２", "HP/Lck +2"), ("SID_ＨＰ・幸運＋４", "HP/Lck +4"), ("SID_ＨＰ・幸運＋６", "HP/Lck +6"),
            ("SID_ＨＰ・幸運＋８", "HP/Lck +8"), ("SID_ＨＰ・幸運＋１０", "HP/Lck +10"), ("SID_力・守備＋１", "Str/Def +1"), ("SID_力・守備＋２", "Str/Def +2"),
            ("SID_力・守備＋３", "Str/Def +3"), ("SID_力・守備＋４", "Str/Def +4"), ("SID_力・守備＋５", "Str/Def +5"), ("SID_魔力・技＋１", "Mag/Dex +1"),
            ("SID_魔力・技＋２", "Mag/Dex +2"), ("SID_魔力・技＋３", "Mag/Dex +3"), ("SID_魔力・技＋４", "Mag/Dex +4"), ("SID_魔力・技＋５", "Mag/Dex +5"),
            ("SID_魔力・魔防＋１", "Mag/Res +1"), ("SID_魔力・魔防＋２", "Mag/Res +2"), ("SID_魔力・魔防＋３", "Mag/Res +3"), ("SID_魔力・魔防＋４", "Mag/Res +4"),
            ("SID_魔力・魔防＋５", "Mag/Res +5"), ("SID_速さ・魔防＋１", "Spd/Res +1"), ("SID_速さ・魔防＋２", "Spd/Res +2"), ("SID_速さ・魔防＋３", "Spd/Res +3"),
            ("SID_速さ・魔防＋４", "Spd/Res +4"), ("SID_速さ・魔防＋５", "Spd/Res +5"), ("SID_技・速さ＋１", "Spd/Dex +1"), ("SID_技・速さ＋２", "Spd/Dex +2"),
            ("SID_技・速さ＋３", "Spd/Dex +3"), ("SID_技・速さ＋４", "Spd/Dex +4"), ("SID_技・速さ＋５", "Spd/Dex +5"), ("SID_対弓術１", "Bow Guard 1"),
            ("SID_対弓術２", "Bow Guard 2"), ("SID_対弓術３", "Bow Guard 3"), ("SID_対弓術４", "Bow Guard 4"), ("SID_対弓術５", "Bow Guard 5"),
            ("SID_対特殊１", "Special Guard 1"), ("SID_対特殊２", "Special Guard 2"), ("SID_対特殊３", "Special Guard 3"), ("SID_対特殊４", "Special Guard 4"),
            ("SID_対特殊５", "Special Guard 5"), ("SID_対斧術１", "Axe Guard 1"), ("SID_対斧術２", "Axe Guard 2"), ("SID_対斧術３", "Axe Guard 3"),
            ("SID_対斧術４", "Axe Guard 4"), ("SID_対斧術５", "Axe Guard 5"), ("SID_対短剣術１", "Knife Guard 1"), ("SID_対短剣術２", "Knife Guard 2"),
            ("SID_対短剣術３", "Knife Guard 3"), ("SID_対短剣術４", "Knife Guard 4"), ("SID_対短剣術５", "Knife Guard 5"), ("SID_対魔道１", "Magic Guard 1"),
            ("SID_対魔道２", "Magic Guard 2"), ("SID_対魔道３", "Magic Guard 3"), ("SID_対魔道４", "Magic Guard 4"), ("SID_対魔道５", "Magic Guard 5"),
            ("SID_対槍術１", "Lance Guard 1"), ("SID_対槍術２", "Lance Guard 2"), ("SID_対槍術３", "Lance Guard 3"), ("SID_対槍術４", "Lance Guard 4"),
            ("SID_対槍術５", "Lance Guard 5"), ("SID_対剣術１", "Sword Guard 1"), ("SID_対剣術２", "Sword Guard 2"), ("SID_対剣術３", "Sword Guard 3"),
            ("SID_対剣術４", "Sword Guard 4"), ("SID_対剣術５", "Sword Guard 5"), ("SID_守護者", "Protective"), ("SID_負けず嫌い", "Rivalry"),
            ("SID_ムードメーカー", "Friendly Boost"), ("SID_生存戦略", "Survival Plan"), ("SID_理想の騎士像", "Knightly Code"),
            ("SID_保身", "Self-Defense"), ("SID_戦場の花", "Fierce Bloom"), ("SID_促す決着", "This Ends Here"), ("SID_能力誇示", "Show-Off"),
            ("SID_傷をつけたわね", "Final Say"), ("SID_密かな支援", "Stealth Assist"), ("SID_王の尊厳", "Dignity of Solm"), ("SID_殺戮者", "Wear Down"),
            ("SID_輸送隊", "Convoy"), ("SID_瘴気の領域", "Miasma Domain "), ("SID_氷の領域", "Frost Domain"), ("SID_裏邪竜ノ娘_兵種スキル", "Resist Emblems"), ("SID_裏邪竜ノ子_兵種スキル", "Spur Emblems"), ("SID_受けるダメージ-50", "Sigil Protection"),
            ("SID_チェインアタック威力軽減＋", "Bond Breaker+")
        };

        internal List<(string id, string name)> VisibleSkills { get; } = new() // GeneralSkills + 
        {
            ("SID_バリア１", "Fell Barrier"), ("SID_バリア２", "Fell Barrier+"), ("SID_バリア３", "Fell Barrier++"), ("SID_バリア４", "Fell Barrier+++"), ("SID_バリア１_ノーマル用", "Dark Barrier"),
            ("SID_バリア２_ノーマル用", "Dark Barrier+"), ("SID_バリア３_ノーマル用", "Dark Barrier++"), ("SID_バリア４_ノーマル用", "Dark Barrier+++"),
            ("SID_双聖", "Sacred Twins"), ("SID_オルタネイト", "Night and Day"),
            ("SID_以心", "Attuned"), ("SID_以心_竜族", "Attuned [Dragon]"), ("SID_以心_連携", "Attuned [Backup]"),
            ("SID_以心_騎馬", "Attuned [Cavalry]"), ("SID_以心_隠密", "Attuned [Covert]"), ("SID_以心_重装", "Attuned [Armored]"), ("SID_以心_飛行", "Attuned [Flying]"),
            ("SID_以心_魔法", "Attuned [Mystical]"), ("SID_以心_気功", "Attuned [Qi Adept]"),
            ("SID_切磋琢磨", "Friendly Rivalry"), ("SID_計略", "Gambit"), ("SID_半身", "Other Half"), ("SID_半身_竜族", "Other Half [Dragon]"),
            ("SID_半身_連携", "Other Half [Backup]"), ("SID_半身_隠密", "Other Half [Covert]"), ("SID_半身_連携_G006_クロム", "Corrupted Other Half"),
            ("SID_守護者_E001", "Protective (Xenologue 1)"),
            ("SID_守護者_E002", "Protective (Xenologue 2)"), ("SID_守護者_E003", "Protective (Xenologue 3)"),
            ("SID_守護者_E004", "Protective (Xenologue 4)"),
            ("SID_役に立ちたい_E001", "Wounded Pride (Xenologue 1)"), ("SID_役に立ちたい_E002", "Wounded Pride (Xenologue 2)"), ("SID_役に立ちたい_E003", "Wounded Pride (Xenologue 3)"), ("SID_役に立ちたい_E004", "Wounded Pride (Xenologue 4)"),
            ("SID_負けず嫌い_E005", "Rivalry (Xenologue 5)"), ("SID_異形狼連携", "Pack Hunter (Corrupted)"),
            ("SID_幻影狼連携", "Pack Hunter (Phantom)"),
        };

        internal List<(string id, string name)> SynchHPSkills { get; } = new()
        {
            ("SID_ＨＰ＋３", "HP +3"), ("SID_ＨＰ＋５", "HP +5"), ("SID_ＨＰ＋７", "HP +7"), ("SID_ＨＰ＋１０", "HP +10"),
            ("SID_ＨＰ＋１２", "HP +12"), ("SID_ＨＰ＋１５", "HP +15")
        };

        internal List<(string id, string name)> SynchStrSkills { get; } = new()
        {
            ("SID_力＋１", "Strength +1"), ("SID_力＋２", "Strength +2"), ("SID_力＋３", "Strength +3"), ("SID_力＋４", "Strength +4"),
            ("SID_力＋５", "Strength +5"), ("SID_力＋６", "Strength +6")
        };

        internal List<(string id, string name)> SynchDexSkills { get; } = new()
        {
            ("SID_技＋１", "Dexterity +1"), ("SID_技＋２", "Dexterity +2"), ("SID_技＋３", "Dexterity +3"), ("SID_技＋４", "Dexterity +4 "),
            ("SID_技＋５", "Dexterity +5")
        };

        internal List<(string id, string name)> SynchSpdSkills { get; } = new()
        {
            ("SID_速さ＋１", "Speed +1"), ("SID_速さ＋２", "Speed +2"), ("SID_速さ＋３", "Speed +3"), ("SID_速さ＋４", "Speed +4"),
            ("SID_速さ＋５", "Speed +5")
        };

        internal List<(string id, string name)> SynchLckSkills { get; } = new()
        {
            ("SID_幸運＋２", "Luck +2"), ("SID_幸運＋４", "Luck +4"), ("SID_幸運＋６", "Luck +6"), ("SID_幸運＋８", "Luck +8"),
            ("SID_幸運＋１０", "Luck +10"), ("SID_幸運＋１２", "Luck +12")
        };

        internal List<(string id, string name)> SynchDefSkills { get; } = new()
        {
            ("SID_守備＋１", "Defense +1"), ("SID_守備＋２", "Defense +2"), ("SID_守備＋３", "Defense +3"), ("SID_守備＋４", "Defense +4"),
            ("SID_守備＋５", "Defense +5")
        };

        internal List<(string id, string name)> SynchMagSkills { get; } = new()
        {
            ("SID_魔力＋１", "Magic +1"), ("SID_魔力＋２", "Magic +2"), ("SID_魔力＋３", "Magic +3"), ("SID_魔力＋４", "Magic +4"),
            ("SID_魔力＋５", "Magic +5")
        };

        internal List<(string id, string name)> SynchResSkills { get; } = new()
        {
            ("SID_魔防＋１", "Resistance +1"), ("SID_魔防＋２", "Resistance +2"), ("SID_魔防＋３", "Resistance +3"), ("SID_魔防＋４", "Resistance +4"),
            ("SID_魔防＋５", "Resistance +5")
        };

        internal List<(string id, string name)> SynchBldSkills { get; } = new()
        {
            ("SID_体格＋１", "Build +1"), ("SID_体格＋２", "Build +2"), ("SID_体格＋３", "Build +3"), ("SID_体格＋４", "Build +4"),
            ("SID_体格＋５", "Build +5 ")
        };

        internal List<(string id, string name)> SynchMovSkills { get; } = new()
        {
            ("SID_移動＋１", "Movement +1")
        };

        internal List<(string id, string name)> SynchStatSkills { get; } = new();

        internal Dictionary<string, ushort> DefaultSPCost { get; } = new()
        {
            { "Movement +1", 1000 }, { "Poison Strike", 300 }, { "Savage Blow", 300 }, { "Swordbreaker", 300 },
            { "Lancebreaker", 300 }, { "Axebreaker", 300 }, { "Tomebreaker", 300 }, { "Bowbreaker", 300 },
            { "Knifebreaker", 300 }, { "Artbreaker", 300 }, { "Seal Strength", 300 }, { "Seal Magic", 300 },
            { "Seal Defense", 300 }, { "Seal Speed", 300 }, { "Seal Resistance", 300 }, { "Fierce Stance", 300 },
            { "Steady Stance", 300 }, { "Darting Stance", 300 }, { "Warding Stance", 300 }, { "Life and Death", 300 },
            { "Triangle Adept", 300 }, { "Cornered Beast", 300 }, { "Self-Destruct", 100 }, { "Duelist's Blow", 1000 },
            { "Darting Blow", 1000 }, { "Death Blow", 1000 }, { "Certain Blow", 1000 }, { "Armored Blow", 1000 },
            { "Warding Blow", 1000 }, { "Spirit Strike", 100 }, { "Unbreakable", 100 }, { "Stalwart", 100 },
            { "Unwavering", 300 }, { "Anchor", 100 }, { "Veteran", 300 }, { "Veteran+", 500 },
            { "Void Curse", 100 }, { "Fell Barrier", 6000 }, { "Fell Barrier+", 8000 }, { "Fell Barrier++", 10000 },
            { "Fell Barrier+++", 12000 }, { "Dark Barrier", 2000 }, { "Dark Barrier+", 4000 }, { "Dark Barrier++", 6000 },
            { "Dark Barrier+++", 8000 }, { "Bond Breaker", 1000 }, { "Divine Speed", 3000 }, { "Divine Speed [Covert]", 3000 },
            { "Divine Speed [Dragon]", 3000 }, { "Gallop", 5000 }, { "Gallop [Dragon]", 5000 }, { "Gallop [Cavalry]", 5000 },
            { "Gallop [Covert]", 5000 }, { "Dark Gallop", 5000 }, { "Unholy Stance", 1000 }, { "Echo", 300 },
            { "Echo [Dragon]", 300 }, { "Echo [Mystical]", 300 }, { "Cleric", 300 }, { "Cleric+", 400 },
            { "Cleric++", 500 }, { "Augment", 3000 }, { "Augment [Dragon]", 3000 }, { "Augment [Qi Adept]", 3000 },
            { "Dark Augment", 3000 }, { "Rise Above", 3000 }, { "Rise Above [Dragon]", 3000 }, { "Rise Above [Cavalry]", 3000 },
            { "Rise Above [Armored]", 3000 }, { "Sink Below", 3000 }, { "Adaptable", 300 }, { "Adaptable [Dragon]", 300 },
            { "Adaptable [Backup]", 300 }, { "Adaptable [Covert]", 300 }, { "Adaptable [Armored]", 300 }, { "Adaptable [Flying]", 300 },
            { "Dual Strike", 1000 }, { "Bonded Shield", 5000 }, { "Bonded Shield [Dragon]", 5000 }, { "Bonded Shield [Cavalry]", 5000 },
            { "Bonded Shield [Armored]", 5000 }, { "Bonded Shield [Flying]", 5000 }, { "Bonded Shield [Qi Adept]", 5000 }, { "Call Doubles", 3000 },
            { "Call Doubles [Dragon]", 3000 }, { "Call Doubles [Flying]", 3000 }, { "Laguz Friend", 5000 }, { "Laguz Friend [Dragon]", 5000 },
            { "Instruct", 1000 }, { "Instruct [Dragon]", 1000 }, { "Instruct [Backup]", 1000 }, { "Instruct [Cavalry]", 1000 },
            { "Instruct [Covert]", 1000 }, { "Instruct [Armored]", 1000 }, { "Instruct [Flying]", 1000 }, { "Instruct [Mystical]", 1000 },
            { "Instruct [Qi Adept]", 1000 }, { "Dragon Vein (Corrin)", 1000 }, { "Dragon Vein (Corrin) [Dragon]", 1000 }, { "Dragon Vein (Corrin) [Backup]", 1000 },
            { "Dragon Vein (Corrin) [Cavalry]", 1000 }, { "Dragon Vein (Corrin) [Covert]", 1000 }, { "Dragon Vein (Corrin) [Armored]", 1000 }, { "Dragon Vein (Corrin) [Flying]", 1000 },
            { "Dragon Vein (Corrin) [Mystical]", 1000 }, { "Dragon Vein (Corrin) [Qi Adept]", 1000 }, { "Dreadful Aura", 5000 }, { "Dreadful Aura [Covert]", 5000 },
            { "Sacred Twins", 100 }, { "Night and Day", 100 }, { "Solar Brace", 3000 }, { "Eclipse Brace", 3000 },
            { "Solar Brace+", 5000 }, { "Eclipse Brace+", 5000 }, { "Bravery", 2000 }, { "Blue Skies", 2000 },
            { "Bravery+", 3000 }, { "Blue Skies+", 3000 }, { "Holy Aura", 100 }, { "Holy Shield", 300 },
            { "Bond Forger", 2000 }, { "Bond Forger+", 3000 }, { "Boon of Elyos", 3000 }, { "Attuned", 3000 },
            { "Attuned [Dragon]", 3000 }, { "Attuned [Backup]", 3000 }, { "Attuned [Cavalry]", 3000 }, { "Attuned [Covert]", 3000 },
            { "Attuned [Armored]", 3000 }, { "Attuned [Flying]", 3000 }, { "Attuned [Mystical]", 3000 }, { "Attuned [Qi Adept]", 3000 },
            { "Divinely Inspiring", 500 }, { "Alabaster Duty", 100 }, { "Verdant Faith", 100 }, { "Crimson Cheer", 100 },
            { "Self-Improver", 100 }, { "Energized", 100 }, { "Moved to Tears", 100 }, { "Gentle Flower", 100 },
            { "Fairy-Tale Folk", 100 }, { "Admiration", 100 }, { "Fair Fight", 300 }, { "Aspiring Hero", 100 },
            { "Meditation", 100 }, { "Get Behind Me!", 300 }, { "Share Spoils", 300 }, { "Generosity", 300 },
            { "Single-Minded", 100 }, { "Not *Quite*", 500 }, { "Blinding Flash", 300 }, { "Big Personality", 500 },
            { "Stunning Smile", 300 }, { "Disarming Sigh", 300 }, { "Racket of Solm", 100 }, { "Knightly Escort", 300 },
            { "Blood Fury", 500 }, { "Charmer", 100 }, { "Party Animal", 300 }, { "Seconds?", 100 },
            { "Make a Killing", 300 }, { "Expertise", 1000 }, { "Trained to Kill", 500 }, { "Curious Dance", 300 },
            { "Weapon Insight", 500 }, { "Will to Win", 100 }, { "Fell Protection", 500 }, { "Contemplative", 100 },
            { "Brave Assist", 1000 }, { "Pincer Attack", 500 }, { "Reforge", 100 }, { "Smash+", 100 },
            { "Merciless", 500 }, { "No Distractions", 500 }, { "Careful Aim", 300 }, { "Swap", 200 },
            { "Allied Defense", 500 }, { "Pivot", 200 }, { "Hobble", 500 }, { "Clear the Way", 500 },
            { "Air Raid", 300 }, { "Pass", 500 }, { "Spell Harmony", 300 }, { "Chaos Style", 500 },
            { "Diffuse Healer", 500 }, { "Self-Healing", 300 }, { "Divine Spirit", 300 }, { "Fell Spirit", 300 },
            { "Dark Spirit", 100 }, { "Golden Lotus", 300 }, { "Ignis", 500 }, { "Sol", 500 },
            { "Luna", 500 }, { "Grasping Void", 300 }, { "World Tree", 500 }, { "Sandstorm", 500 },
            { "Back at You", 100 }, { "Soulblade", 500 }, { "Special Dance", 500 }, { "Sympathetic", 500 },
            { "Deadly Blade", 500 }, { "Battlewise", 300 }, { "Renewal", 500 }, { "Windsweep", 100 },
            { "Great Thunder", 500 }, { "Bane", 100 }, { "Mercy", 100 }, { "Raging Fire", 500 },
            { "Strong Arm", 500 }, { "Miracle", 300 }, { "Dire Thunder", 1000 }, { "Rightful Ruler", 300 },
            { "Healtouch", 100 }, { "Draw Back", 200 }, { "Giga Excalibur", 300 },  { "Wind Adept", 300 },
            { "Shove", 100 }, { "Flickering Flower", 100 }, { "Wind God", 1000 }, { "Chivalry", 300 },
            { "Bushido", 500 }, { "Lethality", 300 }, { "Sure Strike", 300 }, { "Spur Attack", 500 },
            { "Fortify Def", 500 }, { "Spur Res", 500 }, { "Friendly Rivalry", 100 }, { "Gambit", 1500 },
            { "Combat Arts", 1000 }, { "Combat Arts [Dragon]", 1000 }, { "Combat Arts [Covert]", 1000 }, { "Draconic Form", 3000 },
            { "Draconic Form [Armored]", 3000 }, { "Draconic Form [Mystical]", 3000 }, { "Piercing Glare", 1000 }, { "Impenetrable", 3000 },
            { "Impenetrable [Dragon]", 3000 }, { "Impenetrable [Cavalry]", 3000 }, { "Impenetrable [Armored]", 3000 }, { "Impenetrable [Flying]", 3000 },
            { "Contract", 1000 }, { "Contract [Dragon]", 1000 }, { "Contract [Backup]", 1000 }, { "Contract [Covert]", 1000 },
            { "Flare", 3000 }, { "Flare [Dragon]", 3000 }, { "Flare [Mystical]", 3000 }, { "Flare [Qi Adept]", 3000 },
            { "Corrupted Flare", 3000 }, { "Dragon Vein (Camilla)", 1000 }, { "Dragon Vein (Camilla) [Dragon]", 1000 }, { "Dragon Vein (Camilla) [Backup]", 1000 },
            { "Dragon Vein (Camilla) [Cavalry]", 1000 }, { "Dragon Vein (Camilla) [Covert]", 1000 }, { "Dragon Vein (Camilla) [Armored]", 1000 }, { "Dragon Vein (Camilla) [Flying]", 1000 },
            { "Dragon Vein (Camilla) [Mystical]", 1000 }, { "Dragon Vein (Camilla) [Qi Adept]", 1000 }, { "Soar", 3000 }, { "Soar [Dragon]", 3000 },
            { "Soar [Cavalry]", 3000 }, { "Soar [Flying]", 3000 }, { "Other Half", 3000 }, { "Other Half [Dragon]", 3000 },
            { "Other Half [Backup]", 3000 }, { "Other Half [Covert]", 3000 }, { "Corrupted Other Half", 3000 }, { "Other Half (Arena)", 3000 },
            { "Protective", 100 }, { "Protective (Xenologue 1)", 100 }, { "Protective (Xenologue 2)", 100 }, { "Protective (Xenologue 3)", 100 },
            { "Protective (Xenologue 4)", 100 }, { "Rivalry", 100 }, { "Wounded Pride (Xenologue 1)", 100 }, { "Wounded Pride (Xenologue 2)", 100 },
            { "Wounded Pride (Xenologue 3)", 100 }, { "Wounded Pride (Xenologue 4)", 100 }, { "Rivalry (Xenologue 5)", 100 }, { "Friendly Boost", 300 },
            { "Survival Plan", 300 }, { "Knightly Code", 300 }, { "Self-Defense", 100 }, { "Fierce Bloom", 100 },
            { "This Ends Here", 300 }, { "Show-Off", 300 }, { "Final Say", 100 }, { "Stealth Assist", 500 },
            { "Dignity of Solm", 100 }, { "Wear Down", 100 }, { "Convoy", 500 }, { "Miasma Domain ", 100 },
            { "Frost Domain", 100 }, { "Pack Hunter (Corrupted)", 100 }, { "Pack Hunter (Phantom)", 100 }, { "Resist Emblems", 300 },
            { "Spur Emblems", 300 }, { "Sigil Protection", 5000 }, { "Bond Breaker+", 1500 }, { "Lodestar Rush", 5000 },
            { "Lodestar Rush [Dragon]", 5000 }, { "Lodestar Rush [Backup]", 5000 }, { "Lodestar Rush [Mystical]", 5000 }, { "Override", 5000 },
            { "Override [Dragon]", 5000 }, { "Override [Armored]", 5000 }, { "Override [Mystical]", 5000 }, { "Override [Qi Adept]", 5000 },
            { "Warp Ragnarok", 5000 }, { "Warp Ragnarok [Dragon]", 5000 }, { "Warp Ragnarok [Cavalry]", 5000 }, { "Warp Ragnarok [Flying]", 5000 },
            { "Warp Ragnarok [Mystical]", 5000 }, { "Dark Warp", 5000 }, { "Ragnarok Warp", 5000 }, { "Great Sacrifice", 5000 },
            { "Great Sacrifice [Dragon]", 5000 }, { "Great Sacrifice [Armored]", 5000 }, { "Great Sacrifice [Qi Adept]", 5000 }, { "Blazing Lion", 5000 },
            { "Blazing Lion [Dragon]", 5000 }, { "Blazing Lion [Mystical]", 5000 }, { "Quadruple Hit", 5000 }, { "Quadruple Hit [Dragon]", 5000 },
            { "Quadruple Hit [Covert]", 5000 }, { "Quadruple Hit [Qi Adept]", 5000 }, { "All for One", 5000 }, { "All for One [Dragon]", 5000 },
            { "All for One [Backup]", 5000 }, { "Astra Storm", 5000 }, { "Astra Storm [Dragon]", 5000 }, { "Astra Storm [Covert]", 5000 },
            { "Astra Storm [Qi Adept]", 5000 }, { "Weak Astra Storm", 5000 }, { "Weak Astra Storm [Qi Adept]", 5000 }, { "Great Aether", 5000 },
            { "Great Aether [Dragon]", 5000 }, { "Great Aether [Armored]", 5000 }, { "Great Aether [Flying]", 5000 }, { "Goddess Dance", 5000 },
            { "Goddess Dance [Dragon]", 5000 }, { "Goddess Dance [Backup]", 5000 }, { "Goddess Dance [Cavalry]", 5000 }, { "Goddess Dance [Covert]", 5000 },
            { "Goddess Dance [Armored]", 5000 }, { "Goddess Dance [Flying]", 5000 }, { "Goddess Dance [Mystical]", 5000 }, { "Goddess Dance [Qi Adept]", 5000 },
            { "Diabolical Dance", 5000 }, { "Torrential Roar", 5000 }, { "Torrential Roar [Dragon]", 5000 }, { "Twin Strike", 5000 },
            { "Twin Strike [Dragon]", 5000 }, { "Twin Strike [Cavalry]", 5000 }, { "Dragon Blast", 5000 }, { "Dragon Blast [Dragon]", 5000 },
            { "Dragon Blast [Backup]", 5000 }, { "Dragon Blast [Mystical]", 5000 }, { "Dragon Blast [Qi Adept]", 5000 }, { "Bond Blast", 8000 },
            { "Bond Blast [Dragon]", 8000 }, { "Bond Blast [Backup]", 8000 }, { "Bond Blast [Mystical]", 8000 }, { "Bond Blast [Qi Adept]", 8000 },
            { "Run Through", 300 }, { "Paraselene", 300 }, { "Flame Gambit", 1500 }, { "Shield Gambit", 1500 },
            { "Poison Gambit", 1500 }, { "Raging Storm", 1000 }, { "Raging Storm [Dragon]", 1000 }, { "Raging Storm [Covert]", 1000 },
            { "Atrocity", 1000 }, { "Atrocity [Dragon]", 1000 }, { "Atrocity [Covert]", 1000 }, { "Fallen Star", 1000 },
            { "Fallen Star [Dragon]", 1000 }, { "Fallen Star [Covert]", 1000 }, { "Houses Unite", 5000 }, { "Houses Unite [Dragon]", 5000 },
            { "Houses Unite [Cavalry]", 5000 }, { "Houses Unite [Covert]", 5000 }, { "Houses Unite [Armored]", 5000 }, { "Houses Unite [Qi Adept]", 5000 },
            { "Houses Unite+", 8000 }, { "Houses Unite+ [Dragon]", 8000 }, { "Houses Unite+ [Cavalry]", 8000 }, { "Houses Unite+ [Covert]", 8000 },
            { "Houses Unite+ [Armored]", 8000 }, { "Houses Unite+ [Qi Adept]", 8000 }, { "Divine Blessing", 5000 }, { "Divine Blessing [Dragon]", 5000 },
            { "Divine Blessing [Qi Adept]", 5000 }, { "Divine Blessing (Xenologue 1)", 5000 }, { "Divine Blessing+", 8000 }, { "Divine Blessing+ [Dragon]", 8000 },
            { "Divine Blessing+ [Qi Adept]", 8000 }, { "Storm's Eye", 5000 }, { "Storm's Eye [Dragon]", 5000 }, { "Storm's Eye [Backup]", 5000 },
            { "Storm's Eye [Covert]", 5000 }, { "Storm's Eye+", 8000 }, { "Storm's Eye+ [Dragon]", 8000 }, { "Storm's Eye+ [Backup]", 8000 },
            { "Storm's Eye+ [Covert]", 8000 }, { "Summon Hero", 5000 }, { "Summon Hero [Dragon]", 5000 }, { "Summon Hero [Backup]", 5000 },
            { "Summon Hero [Cavalry]", 5000 }, { "Cataclysm", 5000 }, { "Cataclysm [Dragon]", 5000 }, { "Cataclysm [Mystical]", 5000 },
            { "Cataclysm [Qi Adept]", 5000 }, { "Cataclysm+", 8000 }, { "Cataclysm+ [Dragon]", 8000 }, { "Cataclysm+ [Mystical]", 8000 },
            { "Cataclysm+ [Qi Adept]", 8000 }, { "Cataclysm (Divine Paralogue)", 5000 }, { "Corrupted Cataclysm", 5000 }, { "Dark Inferno", 5000 },
            { "Dark Inferno [Dragon]", 5000 }, { "Dark Inferno [Mystical]", 5000 }, { "Dark Inferno [Qi Adept]", 5000 }, { "Dark Inferno+", 8000 },
            { "Dark Inferno+ [Dragon]", 8000 }, { "Dark Inferno+ [Mystical]", 8000 }, { "Dark Inferno+ [Qi Adept]", 8000 }, { "Giga Levin Sword", 5000 },
            { "Giga Levin Sword [Dragon]", 5000 }, { "Giga Levin Sword [Flying]", 5000 }, { "Giga Levin Sword [Mystical]", 5000 }, { "Giga Levin Sword+", 8000 },
            { "Giga Levin Sword+ [Dragon]", 8000 }, { "Giga Levin Sword+ [Flying]", 8000 }, { "Giga Levin Sword+ [Mystical]", 8000 }, { "Let Fly", 300 }
        };

        internal Dictionary<string, string> EngageAttackToBondLinkSkill { get; } = new()
        {
            { "SID_リュールエンゲージ技", "SID_リュールエンゲージ技共同" }, // Dragon Blast
            { "SID_三級長エンゲージ技", "SID_三級長エンゲージ技＋" }, // Houses Unite
            { "SID_チキエンゲージ技", "SID_チキエンゲージ技＋" }, // Divine Blessing
            { "SID_ヘクトルエンゲージ技", "SID_ヘクトルエンゲージ技＋" }, // Storm's Eye
            { "SID_セネリオエンゲージ技", "SID_セネリオエンゲージ技＋" }, // Cataclysm
            { "SID_カミラエンゲージ技", "SID_カミラエンゲージ技＋" }, // Dark Inferno
            { "SID_クロムエンゲージ技", "SID_クロムエンゲージ技＋" } // Giga Levin Sword
        };

        internal Dictionary<string, sbyte> EngageAttackToAIEngageAttackType { get; } = new()
        {
            { "SID_マルスエンゲージ技", 1 }, // Lodestar Rush
            { "SID_シグルドエンゲージ技", 2 }, // Override
            { "SID_セリカエンゲージ技", 1 }, // Warp Ragnarok
            { "SID_ミカヤエンゲージ技", 4 }, // Great Sacrifice
            { "SID_ロイエンゲージ技", 1 }, // Blazing Lion
            { "SID_リーフエンゲージ技", 1 }, // Quadruple Hit
            { "SID_ルキナエンゲージ技", 1 }, // All for One
            { "SID_リンエンゲージ技", 1 }, // Astra Storm
            { "SID_アイクエンゲージ技", 3 }, // Great Aether
            { "SID_ベレトエンゲージ技", 5 }, // Goddess Dance
            { "SID_カムイエンゲージ技", 1 }, // Torrential Roar
            { "SID_エイリークエンゲージ技", 1 }, // Twin Strike
            { "SID_リュールエンゲージ技", 1 }, // Dragon Blast
            { "SID_三級長エンゲージ技", 1 }, // Houses Unite
            { "SID_チキエンゲージ技", 6 }, // Divine Blessing
            { "SID_ヘクトルエンゲージ技", 7 }, // Storm's Eye
            { "SID_ヴェロニカエンゲージ技", 9 }, // Summon Hero
            { "SID_セネリオエンゲージ技", 1 }, // Cataclysm
            { "SID_カミラエンゲージ技", 8 }, // Dark Inferno
            { "SID_クロムエンゲージ技", 1 } // Giga Levin Sword
        };

        internal enum SynchStat { HP, Str, Dex, Spd, Lck, Def, Mag, Res, Bld, Mov, None }

        internal List<List<string>> SynchStatLookup = new();
        #endregion
        #region TalkAnim IDs
        internal List<(string id, string name)> MaleTalkAnims { get; } = new()
        {
            ("AOC_Talk_c000", "Default Male"), ("AOC_Talk_c001", "Male Alear"),
            ("AOC_Talk_c049", "Rafal"), ("AOC_Talk_c100", "Alfred A"),
            ("AOC_Talk_c100b", "Alfred B"), ("AOC_Talk_c101", "Boucheron"),
            ("AOC_Talk_c102", "Louis"), ("AOC_Talk_c103", "Jean"),
            ("AOC_Talk_c200", "Diamant A"), ("AOC_Talk_c200b", "Diamant B"),
            ("AOC_Talk_c201", "Alcryst A"), ("AOC_Talk_c201b", "Alcryst B"),
            ("AOC_Talk_c202", "Morion"), ("AOC_Talk_c203", "Amber"),
            ("AOC_Talk_c300", "Hyacinth"), ("AOC_Talk_c301", "Zelkov"),
            ("AOC_Talk_c302", "Kagetsu"), ("AOC_Talk_c304", "Lindon"),
            ("AOC_Talk_c400", "Fogado A"), ("AOC_Talk_c400b", "Fogado B"),
            ("AOC_Talk_c401", "Pandreo"), ("AOC_Talk_c402", "Bunet"),
            ("AOC_Talk_c403", "Seadall"), ("AOC_Talk_c500", "Vander"),
            ("AOC_Talk_c501", "Clanne"), ("AOC_Talk_c502", "Mauvier"),
            ("AOC_Talk_c503", "Griss"), ("AOC_Talk_c503b", "Gregory"),
            ("AOC_Talk_c530", "Marth"), ("AOC_Talk_c531", "Sigurd"),
            ("AOC_Talk_c532", "Leif"), ("AOC_Talk_c533", "Roy"),
            ("AOC_Talk_c534", "Ike"), ("AOC_Talk_c535", "Byleth"),
            ("AOC_Talk_c536", "Ephraim"), ("AOC_Talk_c514", "Dimitri"),
            ("AOC_Talk_c515", "Claude"), ("AOC_Talk_c510", "Hector"),
            ("AOC_Talk_c511", "Soren"), ("AOC_Talk_c512", "Chrom"),
            ("AOC_Talk_c513", "Robin"),
        };

        internal List<(string id, string name)> FemaleTalkAnims { get; } = new()
        {
            ("AOC_Talk_c050", "Default Female"), ("AOC_Talk_c051", "Female Alear"),
            ("AOC_Talk_c099", "Nel"), ("AOC_Talk_c150", "Céline A"),
            ("AOC_Talk_c150b", "Céline B"), ("AOC_Talk_c151", "Éve"),
            ("AOC_Talk_c152", "Etie"), ("AOC_Talk_c153", "Chloé"),
            ("AOC_Talk_c250", "Jade"), ("AOC_Talk_c251", "Lapis"),
            ("AOC_Talk_c252", "Citrinne"), ("AOC_Talk_c253", "Yunaka"),
            ("AOC_Talk_c254", "Saphir"), ("AOC_Talk_c303", "Rosado"),
            ("AOC_Talk_c350", "Ivy A"), ("AOC_Talk_c350b", "Ivy B"),
            ("AOC_Talk_c351", "Hortensia A"), ("AOC_Talk_c351b", "Hortensia B"),
            ("AOC_Talk_c352", "Goldmary"), ("AOC_Talk_c450", "Timerra A"),
            ("AOC_Talk_c450b", "Timerra B"), ("AOC_Talk_c451", "Seforia"),
            ("AOC_Talk_c452", "Merrin"), ("AOC_Talk_c453", "Panette"),
            ("AOC_Talk_c550", "Framme"), ("AOC_Talk_c551", "Veyle"),
            ("AOC_Talk_c556", "Evil Veyle"), ("AOC_Talk_c552", "Anna"),
            ("AOC_Talk_c553", "Zephia"), ("AOC_Talk_c553b", "Zelestia"),
            ("AOC_Talk_c554", "Marni"), ("AOC_Talk_c554b", "Madeline"),
            ("AOC_Talk_c555", "Lumera"), ("AOC_Talk_c580", "Celica"),
            ("AOC_Talk_c581", "Lyn"), ("AOC_Talk_c582", "Eirika"),
            ("AOC_Talk_c583", "Micaiah"), ("AOC_Talk_c584", "Lucina"),
            ("AOC_Talk_c585", "Corrin"), ("AOC_Talk_c560", "Tiki"),
            ("AOC_Talk_c563", "Edelgard"), ("AOC_Talk_c561", "Camilla"),
            ("AOC_Talk_c562", "Veronica"),
        };
        #endregion
        #region Other
        internal List<(int id, string name)> Proficiencies { get; } = new()
        {
            (0, "None"), (1, "Sword"), (2, "Lance"), (3, "Axe"), (4, "Bow"), (5, "Dagger"), (6, "Tome"), (7, "Staff"),
            (8, "Arts"), (9, "Special")
        };

        internal enum Gender // Ah yes, the four genders:
        {
            Male, Female, Both, Rosado
        }

        internal struct AssetShuffleEntity
        {
            internal string name;
            internal string id;
            internal List<string> alternates;
            internal Gender gender;
            internal string iconID;
            internal bool enemyEmblem;
            internal string nameID;
            internal string faceIconID;
            internal string? thumbnail;
            internal Color hair;
            internal string? eid;

            internal AssetShuffleEntity(string name, string id, List<string> alternates, Gender gender,
                string iconID, bool enemyEmblem, string nameID, string faceIconID, string? thumbnail, Color hair, string? eid)
            {
                this.name = name;
                this.id = id;
                this.alternates = alternates;
                this.gender = gender;
                this.iconID = iconID;
                this.enemyEmblem = enemyEmblem;
                this.nameID = nameID;
                this.faceIconID = faceIconID;
                this.thumbnail = thumbnail;
                this.hair = hair;
                this.eid = eid;
            }
        }

        internal List<AssetShuffleEntity> ProtagonistAssetShuffleData { get; } = new()
        {
            new("Alear", "PID_リュール", new() { "MPID_Lueur_M000", "PID_M000_リュール", "JID_M000_神竜ノ子", "MPID_MorphLueur",
                "PID_デモ用_神竜王リュール"}, Gender.Both,
                "001Lueur", false, "MPID_Lueur", "Face_Lueur", "Lueur", Color.FromArgb(97, 184, 231), null)
        };

        internal List<AssetShuffleEntity> PlayableAssetShuffleData { get; } = new()
        {
            new("Vander", "PID_ヴァンドレ", new() { }, Gender.Male,
                "500Vandre", false, "MPID_Vandre", "Face_DarkEmblem", "Vandre", Color.FromArgb(241, 227, 217), null),
            new("Clanne", "PID_クラン", new() { }, Gender.Male,
                "501Clan", false, "MPID_Clan", "Face_DarkEmblem", "Clan", Color.FromArgb(224, 196, 189), null),
            new("Framme", "PID_フラン", new() { }, Gender.Female,
                "550Fram", false, "MPID_Fram", "Face_DarkEmblem", "Fram", Color.FromArgb(217, 212, 201), null),
            new("Alfred", "PID_アルフレッド", new() { "PID_E002_Boss", "PID_E006_Hide1" }, Gender.Male,
                "100Alfred", false, "MPID_Alfred", "Face_DarkEmblem", "Alfred", Color.FromArgb(241, 238, 199), null),
            new("Etie", "PID_エーティエ", new() { }, Gender.Female,
                "152Etie", false, "MPID_Etie", "Face_DarkEmblem", "Etie", Color.FromArgb(250, 170, 104), null),
            new("Boucheron", "PID_ブシュロン", new() { }, Gender.Male,
                "101Boucheron", false, "MPID_Boucheron", "Face_DarkEmblem", "Boucheron", Color.FromArgb(184, 134, 75), null),
            new("Céline", "PID_セリーヌ", new() { "PID_E002_Hide", "PID_E006_Hide2" }, Gender.Female,
                "150Celine", false, "MPID_Celine", "Face_DarkEmblem", "Celine", Color.FromArgb(236, 220, 181), null),
            new("Chloé", "PID_クロエ", new() { }, Gender.Female,
                "153Chloe", false, "MPID_Chloe", "Face_DarkEmblem", "Chloe", Color.FromArgb(90, 180, 180), null),
            new("Louis", "PID_ルイ", new() { }, Gender.Male,
                "102Louis", false, "MPID_Louis", "Face_DarkEmblem", "Louis", Color.FromArgb(66, 50, 35), null),
            new("Yunaka", "PID_ユナカ", new() { }, Gender.Female,
                "253Yunaka", false, "MPID_Yunaka", "Face_DarkEmblem", "Yunaka", Color.FromArgb(170, 22, 71), null),
            new("Alcryst", "PID_スタルーク", new() { "PID_E003_Hide", "PID_E006_Hide4" }, Gender.Male,
                "201Staluke", false, "MPID_Staluke", "Face_DarkEmblem", "Staluke", Color.FromArgb(15, 44, 86), null),
            new("Citrinne", "PID_シトリニカ", new() { }, Gender.Female,
                "252Citrinica", false, "MPID_Citrinica", "Face_DarkEmblem", "Citrinica", Color.FromArgb(226, 216, 161), null),
            new("Lapis", "PID_ラピス", new() { }, Gender.Female,
                "251Lapis", false, "MPID_Lapis", "Face_DarkEmblem", "Lapis", Color.FromArgb(255, 202, 196), null),
            new("Diamant", "PID_ディアマンド", new() { "PID_E003_Boss", "PID_E006_Hide3" }, Gender.Male,
                "200Diamand", false, "MPID_Diamand", "Face_DarkEmblem", "Diamand", Color.FromArgb(124, 42, 42), null),
            new("Amber", "PID_アンバー", new() { }, Gender.Male,
                "203Umber", false, "MPID_Umber", "Face_DarkEmblem", "Umber", Color.FromArgb(246, 198, 77), null),
            new("Jade", "PID_ジェーデ", new() { "PID_ジェーデ_兜あり" }, Gender.Female,
                "250Jade", false, "MPID_Jade", "Face_DarkEmblem", "Jade", Color.FromArgb(255, 229, 160), null),
            new("Ivy", "PID_アイビー", new() { "PID_M008_アイビー", "PID_M009_アイビー", "PID_E004_Boss", "PID_E006_Hide5" }, Gender.Female,
                "350Ivy", false, "MPID_Ivy", "Face_DarkEmblem", "Ivy", Color.FromArgb(132, 37, 108), null),
            new("Kagetsu", "PID_カゲツ", new() { "PID_M008_カゲツ", "PID_M009_カゲツ" }, Gender.Male,
                "302Kagetsu", false, "MPID_Kagetsu", "Face_DarkEmblem", "Kagetsu", Color.FromArgb(56, 65, 98), null),
            new("Zelkov", "PID_ゼルコバ", new() { "PID_M008_ゼルコバ", "PID_M009_ゼルコバ" }, Gender.Male,
                "301Zelkova", false, "MPID_Zelkova", "Face_DarkEmblem", "Zelkova", Color.FromArgb(62, 61, 72), null),
            new("Fogado", "PID_フォガート", new() { "PID_E005_Hide2", "PID_E006_Hide8" }, Gender.Male,
                "400Fogato", false, "MPID_Fogato", "Face_DarkEmblem", "Fogato", Color.FromArgb(60, 33, 19), null),
            new("Pandreo", "PID_パンドロ", new() { }, Gender.Male,
                "401Pandoro", false, "MPID_Pandoro", "Face_DarkEmblem", "Pandoro", Color.FromArgb(246, 127, 83), null),
            new("Bunet", "PID_ボネ", new() { }, Gender.Male,
                "402Bonet", false, "MPID_Bonet", "Face_DarkEmblem", "Bonet", Color.FromArgb(236, 220, 181), null),
            new("Timerra", "PID_ミスティラ", new() { "PID_E004_Hide", "PID_E006_Hide7" }, Gender.Female,
                "450Misutira", false, "MPID_Misutira", "Face_DarkEmblem", "Misutira", Color.FromArgb(60, 33, 19), null),
            new("Panette", "PID_パネトネ", new() { }, Gender.Female,
                "453Panetone", false, "MPID_Panetone", "Face_DarkEmblem", "Panetone", Color.FromArgb(245, 90, 51), null),
            new("Merrin", "PID_メリン", new() { }, Gender.Female,
                "452Merin", false, "MPID_Merin", "Face_DarkEmblem", "Merin", Color.FromArgb(236, 220, 181), null),
            new("Hortensia", "PID_オルテンシア", new() { "PID_M014_オルテンシア", "PID_M007_オルテンシア", "PID_M010_オルテンシア",
                "PID_E005_Hide1", "PID_E006_Hide6" }, Gender.Female,
                "351Hortensia", false, "MPID_Hortensia", "Face_DarkEmblem", "Hortensia", Color.FromArgb(255, 96, 192), null),
            new("Seadall", "PID_セアダス", new() { }, Gender.Male,
                "403Seadas", false, "MPID_Seadas", "Face_DarkEmblem", "Seadas", Color.FromArgb(128, 128, 96), null),
            new("Rosado", "PID_ロサード", new() { "PID_M007_ロサード", "PID_M010_ロサード" }, Gender.Rosado,
                "303Rosado", false, "MPID_Rosado", "Face_DarkEmblem", "Rosado", Color.FromArgb(224, 224, 255), null),
            new("Goldmary", "PID_ゴルドマリー", new() { "PID_M007_ゴルドマリー", "PID_M010_ゴルドマリー" }, Gender.Female,
                "352Goldmary", false, "MPID_Goldmary", "Face_DarkEmblem", "Goldmary", Color.FromArgb(184, 144, 105), null),
            new("Lindon", "PID_リンデン", new() { }, Gender.Male,
                "304Linden", false, "MPID_Linden", "Face_DarkEmblem", "Linden", Color.FromArgb(239, 227, 211), null),
            new("Saphir", "PID_ザフィーア", new() { }, Gender.Female,
                "254Saphir", false, "MPID_Saphir", "Face_DarkEmblem", "Saphir", Color.FromArgb(191, 191, 191), null),
            new("Veyle", "PID_ヴェイル", new() { "PID_ヴェイル_包帯", "PID_ヴェイル_黒_善", "PID_ヴェイル_黒_善_角折れ", "PID_ヴェイル_白_悪",
                "PID_ヴェイル_黒_悪", "PID_M011_ヴェイル", "PID_M017_ヴェイル", "PID_M021_ヴェイル" }, Gender.Female,
                "551Veyre", false, "MPID_Veyre", "Face_DarkEmblem", "Veyre", Color.FromArgb(224, 192, 255), null),
            new("Mauvier", "PID_モーヴ", new() { "PID_M011_モーヴ", "PID_M014_モーヴ", "PID_M017_モーヴ", "PID_M019_モーヴ" }, Gender.Male,
                "502Mauve", false, "MPID_Mauve", "Face_DarkEmblem", "Mauve", Color.FromArgb(88, 91, 102), null),
            new("Anna", "PID_アンナ", new() { }, Gender.Female,
                "552Anna", false, "MPID_Anna", "Face_DarkEmblem", "Anna", Color.FromArgb(196, 85, 81), null),
            new("Jean", "PID_ジャン", new() { }, Gender.Male,
                "103Jean", false, "MPID_Jean", "Face_DarkEmblem", "Jean", Color.FromArgb(92, 95, 109), null),
            new("Rafal", "PID_ラファール", new() { "PID_デモ用_竜石なし_ラファール", "MPID_Il", "PID_E005_Boss", "PID_イル", "PID_E001_イル",
                "PID_E002_イル", "PID_E003_イル", "PID_E004_イル" }, Gender.Male,
                "049Il", false, "MPID_Rafale", "Face_DarkEmblem", "Rafale", Color.FromArgb(255, 255, 255), null),
            new("Nel", "PID_エル", new() { "PID_E001_エル", "PID_E002_エル", "PID_E003_エル", "PID_E004_エル", "PID_E005_エル", "PID_E006_エル" }, Gender.Female,
                "099El", false, "MPID_El", "Face_DarkEmblem", "El", Color.FromArgb(160, 160, 160), null),
            new("Zelestia", "PID_セレスティア", new() { "PID_E002_セレスティア", "PID_E003_セレスティア", "PID_E004_セレスティア",
                "PID_E006_セレスティア" }, Gender.Female,
                "553Selestia", false, "MPID_Selestia", "Face_DarkEmblem", "Selestia", Color.FromArgb(233, 226, 215), null),
            new("Gregory", "PID_グレゴリー", new() { "PID_E003_グレゴリー", "PID_E004_グレゴリー", "PID_E006_グレゴリー" }, Gender.Male,
                "503Gregory", false, "MPID_Gregory", "Face_DarkEmblem", "Gregory", Color.FromArgb(50, 83, 69), null),
            new("Madeline", "PID_マデリーン", new() { "PID_E004_マデリーン", "PID_E006_マデリーン" }, Gender.Female,
                "554Madeline", false, "MPID_Madeline", "Face_DarkEmblem", "Madeline", Color.FromArgb(246, 228, 166), null)
        };

        internal List<AssetShuffleEntity> NamedNPCAssetShuffleData { get; } = new()
        {
            new("Lumera", "PID_ルミエル", new() { "PID_M025_ルミエル", "MPID_MorphLumiere", "PID_M002_ルミエル" }, Gender.Female,
                "555Lumiere", false, "MPID_Lumiere", "Face_DarkEmblem", null, Color.FromArgb(125, 175, 255), null),
            new("Sombron", "PID_ソンブル", new() { "PID_M000_ソンブル", "PID_M026_ソンブル_人型" }, Gender.Male,
                "504Sombre", false, "MPID_Sombre", "Face_DarkEmblem", null, Color.FromArgb(96, 32, 96), null),
            new("Éve", "PID_イヴ", new() { }, Gender.Female,
                "151Eve", false, "MPID_Eve", "Face_DarkEmblem", null, Color.FromArgb(255, 235, 188), null),
            new("Morion", "PID_モリオン", new() { }, Gender.Male,
                "202Morion", false, "MPID_Morion", "Face_DarkEmblem", null, Color.FromArgb(149, 43, 47), null),
            new("Hyacinth", "PID_ハイアシンス", new() { "MPID_MorphHyacinth", "PID_M010_ハイアシンス", "PID_M017_異形兵_ハイアシンス" }, Gender.Male,
                "300Hyacinth", false, "MPID_Hyacinth", "Face_DarkEmblem", null, Color.FromArgb(214, 214, 214), null),
            new("Seforia", "PID_スフォリア", new() { }, Gender.Female,
                "451Sfoglia", false, "MPID_Sfoglia", "Face_DarkEmblem", null, Color.FromArgb(60, 33, 19), null),
            new("Zephia", "PID_セピア", new() { "PID_M011_セピア", "PID_M014_セピア", "PID_M017_セピア", "PID_M021_セピア", "PID_M023_セピア" }, Gender.Female,
                "553Sepia", false, "MPID_Sepia", "Face_DarkEmblem", null, Color.FromArgb(233, 226, 215), null),
            new("Griss", "PID_グリ", new() { "PID_M011_グリ", "PID_M017_グリ", "PID_M020_グリ", "PID_M021_グリ", "PID_M023_グリ" }, Gender.Male,
                "503Gris", false, "MPID_Gris", "Face_DarkEmblem", null, Color.FromArgb(50, 83, 69), null),
            new("Marni", "PID_マロン", new() { "PID_M011_マロン", "PID_M014_マロン", "PID_M017_マロン", "PID_M019_マロン" }, Gender.Female,
                "554Marron", false, "MPID_Marron", "Face_DarkEmblem", null, Color.FromArgb(246, 228, 166), null),
            new("Abyme", "PID_M003_イルシオン兵_ボス", new() { "PID_M018_イルシオン兵_ボス" }, Gender.Female,
                "855Boss", false, "MPID_M003_Boss", "Face_DarkEmblem", null, Color.FromArgb(192, 196, 173), null),
            new("Rodine", "PID_M004_イルシオン兵_ボス", new() { }, Gender.Male,
                "811Boss", false, "MPID_M004_Boss", "Face_DarkEmblem", null, Color.FromArgb(201, 183, 159), null),
            new("Sean", "PID_S001_ジャン_父親", new() { }, Gender.Male,
                "800VillagerMB", false, "MPID_JeanFather", "Face_DarkEmblem", null, Color.FromArgb(92, 95, 109), null),
            new("Anje", "PID_S001_ジャン_母親", new() { }, Gender.Female,
                "850VillagerFB", false, "MPID_JeanMother", "Face_DarkEmblem", null, Color.FromArgb(79, 68, 100), null),
            new("Nelucce", "PID_M005_Irc_ボス", new() { }, Gender.Male,
                "812Boss", false, "MPID_M005_Boss", "Face_DarkEmblem", null, Color.FromArgb(255, 255, 255), null),
            new("Teronda", "PID_M006_ボス", new() { }, Gender.Male,
                "809Boss", false, "MPID_M006_Boss", "Face_DarkEmblem", null, Color.FromArgb(142, 115, 97), null),
            new("Mitan", "PID_S002_蛮族_お頭", new() { }, Gender.Female,
                "810Boss", false, "MPID_S002_Boss", "Face_DarkEmblem", null, Color.FromArgb(73, 73, 73), null),
            new("Corrupted Morion", "PID_M010_異形兵_モリオン", new() { }, Gender.Male,
                "202Morion", false, "MPID_MorphMorion", "Face_DarkEmblem", null, Color.FromArgb(149, 43, 47), null),
            new("Tetchie", "PID_M013_蛮族_お頭Ａ", new() { }, Gender.Male,
                "807Boss", false, "MPID_M013_BossA", "Face_DarkEmblem", null, Color.FromArgb(25, 25, 25), null),
            new("Totchie", "PID_M013_蛮族_お頭Ｂ", new() { }, Gender.Male,
                "808Boss", false, "MPID_M013_BossB", "Face_DarkEmblem", null, Color.FromArgb(25, 25, 25), null),
            new("Past Alear", "JID_邪竜ノ子", new() { "PID_M024_リュール" }, Gender.Both,
                "002Lueur", false, "MPID_PastLueur", "Face_Lueur", "Lueur", Color.FromArgb(97, 184, 231), null),
            new("Durthon", "PID_武器屋", new() { }, Gender.Male,
                "800VillagerMB", false, "MPID_WeaponShop", "Face_DarkEmblem", null, Color.FromArgb(0, 0, 0), null),
            new("Anisse", "PID_道具屋", new() { }, Gender.Female,
                "850VillagerFB", false, "MPID_ItemShop", "Face_DarkEmblem", null, Color.FromArgb(35, 42, 63), null),
            new("Pinet", "PID_アクセ屋", new() { }, Gender.Male,
                "800VillagerMB", false, "MPID_AccessoriesShop", "Face_DarkEmblem", null, Color.FromArgb(192, 117, 114), null),
            new("Calney", "PID_錬成屋", new() { }, Gender.Female,
                "850VillagerFB", false, "MPID_BlackSmith", "Face_DarkEmblem", null, Color.FromArgb(197, 179, 141), null)
        };

        internal List<AssetShuffleEntity> AllyEmblemAssetShuffleData { get; } = new()
        {
            new("Marth", "GID_マルス", new() { "PID_S014_マルス", "PID_闘技場_マルス", "GID_M000_マルス", "GID_相手マルス" }, Gender.Male,
                "530Marth", false, "MGID_Marth", "Face_Marth", "Marth", Color.FromArgb(129, 198, 255), "EID_マルス"),
            new("Sigurd", "GID_シグルド", new() { "PID_S009_シグルド", "PID_闘技場_シグルド", "GID_M002_シグルド", "GID_相手シグルド" }, Gender.Male,
                "531Siglud", false, "MGID_Siglud", "Face_Siglud", "Siglud", Color.FromArgb(201, 216, 255), "EID_シグルド"),
            new("Celica", "GID_セリカ", new() { "PID_S013_セリカ", "PID_闘技場_セリカ", "GID_相手セリカ" }, Gender.Female,
                "580Celica", false, "MGID_Celica", "Face_Celica", "Celica", Color.FromArgb(255, 212, 245), "EID_セリカ"),
            new("Micaiah", "GID_ミカヤ", new() { "PID_S011_ミカヤ", "PID_闘技場_ミカヤ", "GID_相手ミカヤ" }, Gender.Female,
                "583Micaiah", false, "MGID_Micaiah", "Face_Micaiah", "Micaiah", Color.FromArgb(255, 240, 197), "EID_ミカヤ"),
            new("Roy", "GID_ロイ", new() { "PID_S012_ロイ", "PID_闘技場_ロイ", "GID_相手ロイ" }, Gender.Male,
                "533Roy", false, "MGID_Roy", "Face_Roy", "Roy", Color.FromArgb(254, 200, 140), "EID_ロイ"),
            new("Leif", "GID_リーフ", new() { "PID_S010_リーフ", "PID_闘技場_リーフ", "GID_相手リーフ" }, Gender.Male,
                "532Leaf", false, "MGID_Leaf", "Face_Leaf", "Leaf", Color.FromArgb(251, 254, 209), "EID_リーフ"),
            new("Lucina", "GID_ルキナ", new() { "PID_S003_ルキナ", "PID_闘技場_ルキナ", "GID_相手ルキナ" }, Gender.Female,
                "584Lucina", false, "MGID_Lucina", "Face_Lucina", "Lucina", Color.FromArgb(182, 234, 255), "EID_ルキナ"),
            new("Lyn", "GID_リン", new() { "PID_S004_リン", "PID_闘技場_リン", "GID_相手リン" }, Gender.Female,
                "581Lin", false, "MGID_Lin", "Face_Lin", "Lin", Color.FromArgb(216, 241, 178), "EID_リン"),
            new("Ike", "GID_アイク", new() { "PID_S005_アイク", "PID_闘技場_アイク", "GID_相手アイク" }, Gender.Male,
                "534Ike", false, "MGID_Ike", "Face_Ike", "Ike", Color.FromArgb(88, 90, 229), "EID_アイク"),
            new("Byleth", "GID_ベレト", new() { "PID_S006_ベレト", "PID_闘技場_ベレト", "GID_相手ベレト" }, Gender.Male,
                "535Byleth", false, "MGID_Byleth", "Face_Byleth", "Byleth", Color.FromArgb(236, 141, 255), "EID_ベレト"),
            new("Corrin", "GID_カムイ", new() { "PID_S007_カムイ", "PID_M015_カムイ", "PID_闘技場_カムイ", "GID_相手カムイ" }, Gender.Female,
                "585Kamui", false, "MGID_Kamui", "Face_Kamui", "Kamui", Color.FromArgb(188, 188, 188), "EID_カムイ"),
            new("Eirika", "GID_エイリーク", new() { "PID_S008_エイリーク", "PID_闘技場_エイリーク", "GID_相手エイリーク" }, Gender.Female,
                "582Eirik", false, "MGID_Eirik", "Face_Eirik", "Eirik", Color.FromArgb(175, 246, 230), "EID_エイリーク"),
            new("Ephraim", "GID_エフラム", new() { "GID_相手エフラム" }, Gender.Male,
                "536Ephraim", false, "MGID_Ephraim", "Face_Ephraim", "Ephraim", Color.FromArgb(175, 246, 230), null),
            new("Emblem Alear", "GID_リュール", new() { "PID_青リュール", "GID_相手リュール" }, Gender.Both,
                "001Lueur", false, "MGID_Lueur", "Face_Lueur", "Lueur", Color.FromArgb(97, 184, 231), "EID_リュール"),
            new("Edelgard", "GID_エーデルガルト", new() { "GID_相手エーデルガルト" }, Gender.Female,
                "563Edelgard", false, "MGID_Edelgard", "Face_Edelgard", "Edelgard", Color.FromArgb(78, 74, 107), "EID_エーデルガルト"),
            new("Dimitri", "GID_ディミトリ", new() { "GID_相手ディミトリ" }, Gender.Male,
                "514Dimitri", false, "MGID_Dimitri", "Face_Dimitri", "Dimitri", Color.FromArgb(78, 74, 107), null),
            new("Claude", "GID_クロード", new() { "GID_相手クロード" }, Gender.Male,
                "515Claude", false, "MGID_Claude", "Face_Claude", "Claude", Color.FromArgb(78, 74, 107), null),
            new("Tiki", "GID_チキ", new() { "PID_G001_チキ", "PID_G001_チキ_特効無効", "GID_相手チキ" }, Gender.Female,
                "560Tiki", false, "MGID_Tiki", "Face_Tiki", "Tiki", Color.FromArgb(160, 224, 160), "EID_チキ"),
            new("Hector", "GID_ヘクトル", new() { "GID_相手ヘクトル" }, Gender.Male,
                "510Hector", false, "MGID_Hector", "Face_Hector", "Hector", Color.FromArgb(46, 51, 143), "EID_ヘクトル"),
            new("Veronica", "GID_ヴェロニカ", new() { "GID_相手ヴェロニカ" }, Gender.Female,
                "562Veronica", false, "MGID_Veronica", "Face_Veronica", "Veronica", Color.FromArgb(214, 207, 197), "EID_ヴェロニカ"),
            new("Soren", "GID_セネリオ", new() { "GID_相手セネリオ" }, Gender.Male,
                "511Senerio", false, "MGID_Senerio", "Face_Senerio", "Senerio", Color.FromArgb(85, 134, 134), "EID_セネリオ"),
            new("Camilla", "GID_カミラ", new() { "GID_相手カミラ" }, Gender.Female,
                "561Camilla", false, "MGID_Camilla", "Face_Camilla", "Camilla", Color.FromArgb(191, 183, 224), "EID_カミラ"),
            new("Chrom", "GID_クロム", new() { "GID_相手クロム" }, Gender.Male,
                "512Chrom", false, "MGID_Chrom", "Face_Chrom", "Chrom", Color.FromArgb(48, 92, 129), "EID_クロム"),
            new("Robin", "PID_ルフレ", new() { }, Gender.Male,
                "513Robin", false, "MGID_Reflet", "Face_Reflet", "Reflet", Color.FromArgb(48, 92, 129), null),
        };

        internal List<AssetShuffleEntity> EnemyEmblemAssetShuffleData { get; } = new()
        {
            new("Corrupted Marth", "GID_M011_敵マルス", new() { "PID_E003_召喚_マルス", "PID_E006_召喚_マルス", "GID_M017_敵マルス",
                "GID_M021_敵マルス", "GID_M024_敵マルス" }, Gender.Male,
                "530Marth", true, "MGID_Marth", "Face_MarthDarkness", "Marth", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Sigurd", "GID_M011_敵シグルド", new() { "PID_E003_召喚_シグルド", "PID_E006_召喚_シグルド", "GID_M017_敵シグルド",
                "PID_M022_紋章士_シグルド" }, Gender.Male,
                "531Siglud", true, "MGID_Siglud", "Face_SigludDarkness", "Siglud", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Celica", "GID_M011_敵セリカ", new() { "PID_E003_召喚_セリカ", "PID_E006_召喚_セリカ", "GID_M017_敵セリカ",
                "GID_M020_敵セリカ", "PID_M022_紋章士_セリカ" }, Gender.Female,
                "580Celica", true, "MGID_Celica", "Face_CelicaDarkness", "Celica", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Micaiah", "GID_M011_敵ミカヤ", new() { "GID_M017_敵ミカヤ", "GID_M019_敵ミカヤ", "PID_M022_紋章士_ミカヤ" }, Gender.Female,
                "583Micaiah", true, "MGID_Micaiah", "Face_MicaiahDarkness", "Micaiah", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Roy", "GID_M011_敵ロイ", new() { "PID_E006_召喚_ロイ", "GID_M017_敵ロイ", "GID_M019_敵ロイ", "PID_M022_紋章士_ロイ" }, Gender.Male,
                "533Roy", true, "MGID_Roy", "Face_RoyDarkness", "Roy", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Leif", "GID_M008_敵リーフ", new() { "PID_E006_召喚_リーフ", "GID_M011_敵リーフ", "GID_M017_敵リーフ",
                "PID_M022_紋章士_リーフ" }, Gender.Male,
                "532Leaf", true, "MGID_Leaf", "Face_LeafDarkness", "Leaf", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Lucina", "GID_M007_敵ルキナ", new() { "PID_M022_紋章士_ルキナ" }, Gender.Female,
                "584Lucina", true, "MGID_Lucina", "Face_LucinaDarkness", "Lucina", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Lyn", "GID_M010_敵リン", new() { "PID_M022_紋章士_リン" }, Gender.Female,
                "581Lin", true, "MGID_Lin", "Face_LinDarkness", "Lin", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Ike", "PID_M022_紋章士_アイク", new() { }, Gender.Male,
                "534Ike", true, "MGID_Ike", "Face_IkeDarkness", "Ike", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Byleth", "GID_M010_敵ベレト", new() { "GID_M014_敵ベレト", "PID_M022_紋章士_ベレト" }, Gender.Male,
                "535Byleth", true, "MGID_Byleth", "Face_BylethDarkness", "Byleth", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Corrin", "PID_M022_紋章士_カムイ", new() { }, Gender.Female,
                "585Kamui", true, "MGID_Kamui", "Face_KamuiDarkness", "Kamui", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Eirika", "PID_M022_紋章士_エイリーク", new() { }, Gender.Female,
                "582Eirik", true, "MGID_Eirik", "Face_EirikDarkness", "Eirik", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Edelgard", "GID_E006_敵エーデルガルト", new() { }, Gender.Female,
                "563Edelgard", true, "MGID_Edelgard", "Face_EdelgardDarkness", "Edelgard", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Dimitri", "GID_E006_敵ディミトリ", new() { }, Gender.Male,
                "514Dimitri", true, "MGID_Dimitri", "Face_DimitriDarkness", "Dimitri", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Claude", "GID_E006_敵クロード", new() { }, Gender.Male,
                "515Claude", true, "MGID_Claude", "Face_ClaudeDarkness", "Claude", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Tiki", "GID_E001_敵チキ", new() { "GID_E006_敵チキ" }, Gender.Female,
                "560Tiki", true, "MGID_Tiki", "Face_TikiDarkness", "Tiki", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Hector", "GID_E002_敵ヘクトル", new() { "GID_E005_敵ヘクトル", "GID_E006_敵ヘクトル" }, Gender.Male,
                "510Hector", true, "MGID_Hector", "Face_HectorDarkness", "Hector", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Veronica", "GID_E003_敵ヴェロニカ", new() { "GID_E005_敵ヴェロニカ", "GID_E006_敵ヴェロニカ" }, Gender.Female,
                "562Veronica", true, "MGID_Veronica", "Face_VeronicaDarkness", "Veronica", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Soren", "GID_E004_敵セネリオ", new() { "GID_E006_敵セネリオ" }, Gender.Male,
                "511Senerio", true, "MGID_Senerio", "Face_SenerioDarkness", "Senerio", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Camilla", "GID_E004_敵カミラ", new() { "GID_E006_敵カミラ" }, Gender.Female,
                "561Camilla", true, "MGID_Camilla", "Face_CamillaDarkness", "Camilla", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Chrom", "GID_E005_敵クロム", new() { "GID_E006_敵クロム" }, Gender.Male,
                "512Chrom", true, "MGID_Chrom", "Face_ChromDarkness", "Chrom", Color.FromArgb(255, 0, 0), null),
            new("Corrupted Robin", "PID_闇ルフレ", new() { }, Gender.Male,
                "513Robin", true, "MGID_Reflet", "Face_RefletDarkness", "Reflet", Color.FromArgb(255, 0, 0), null),
        };
        #endregion

        internal GameData(XmlParser xp, FileManager fm)
        {
            XP = xp;
            FM = fm;
            BondLevels.AddRange(BondLevelsFromExp);
            AllyBondLevelTables.AddRange(InheritableBondLevelTables);
            BondLevelTables.AddRange(AllyBondLevelTables);
            BondLevelTables.AddRange(EnemyBondLevelTables);
            AllDressModels.AddRange(MaleClassDressModels);
            AllDressModels.AddRange(FemaleClassDressModels);
            AllDressModels.AddRange(MaleCorruptedClassDressModels);
            AllDressModels.AddRange(FemaleCorruptedClassDressModels);
            AllDressModels.AddRange(MalePersonalDressModels);
            AllDressModels.AddRange(FemalePersonalDressModels);
            AllDressModels.AddRange(MaleEmblemDressModels);
            AllDressModels.AddRange(FemaleEmblemDressModels);
            AllDressModels.AddRange(MaleEngageDressModels);
            AllDressModels.AddRange(FemaleEngageDressModels);
            AllDressModels.AddRange(MaleCommonDressModels);
            AllDressModels.AddRange(FemaleCommonDressModels);
            AllyEngageableEmblems.AddRange(AlearEmblems);
            AllyEngageableEmblems.AddRange(LinkableEmblems);
            AllySynchableEmblems.AddRange(AllyEngageableEmblems);
            EnemySynchableEmblems.AddRange(EnemyEngageableEmblems);
            EngageableEmblems.AddRange(AllyEngageableEmblems);
            EngageableEmblems.AddRange(EnemyEngageableEmblems);
            ArenaEmblems.AddRange(BaseArenaEmblems);
            AllyArenaSynchableEmblems.AddRange(AllySynchableEmblems);
            AllyArenaSynchableEmblems.AddRange(ArenaEmblems);
            SynchableEmblems.AddRange(AllyArenaSynchableEmblems);
            SynchableEmblems.AddRange(EnemySynchableEmblems);
            Emblems.AddRange(SynchableEmblems);
            CompatibleAsEngageAttacks.AddRange(TriggerAttackSkills);
            GeneralSkills.AddRange(TriggerAttackSkills);
            VisibleSkills.AddRange(GeneralSkills);
            SynchStatSkills.AddRange(SynchHPSkills);
            SynchStatSkills.AddRange(SynchStrSkills);
            SynchStatSkills.AddRange(SynchDexSkills);
            SynchStatSkills.AddRange(SynchSpdSkills);
            SynchStatSkills.AddRange(SynchLckSkills);
            SynchStatSkills.AddRange(SynchDefSkills);
            SynchStatSkills.AddRange(SynchMagSkills);
            SynchStatSkills.AddRange(SynchResSkills);
            SynchStatSkills.AddRange(SynchBldSkills);
            SynchStatSkills.AddRange(SynchMovSkills);

            SynchStatLookup.Add(SynchHPSkills.GetIDs());
            SynchStatLookup.Add(SynchStrSkills.GetIDs());
            SynchStatLookup.Add(SynchDexSkills.GetIDs());
            SynchStatLookup.Add(SynchSpdSkills.GetIDs());
            SynchStatLookup.Add(SynchLckSkills.GetIDs());
            SynchStatLookup.Add(SynchDefSkills.GetIDs());
            SynchStatLookup.Add(SynchMagSkills.GetIDs());
            SynchStatLookup.Add(SynchResSkills.GetIDs());
            SynchStatLookup.Add(SynchBldSkills.GetIDs());
            SynchStatLookup.Add(SynchMovSkills.GetIDs());
        }
    }

    internal static class GameDataLookup
    {
        internal static List<T> FilterData<T>(this IEnumerable<T> data, Func<T, string> getID, List<(string id, string name)> entities) =>
            data.Where(o => entities.Select(t => t.id).Contains(getID(o))).ToList();
        internal static List<T> GetIDs<T>(this List<(T id, string name)> entities) => entities.Select(t => t.id).ToList();
        internal static string IDToName<T>(this List<(T id, string name)> entities, T id) => entities.First(t => t.id!.Equals(id)).name;
        internal static Dictionary<RandomizerDistribution, DataSetEnum> DistributionToDataSet { get; } = new()
        {
            { RandomizerDistribution.Link, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngageCount, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngageAttackAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngageAttackEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngageAttackLink, DataSetEnum.GodGeneral },
            { RandomizerDistribution.LinkGid, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngravePower, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngraveWeight, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngraveHit, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngraveCritical, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngraveAvoid, DataSetEnum.GodGeneral },
            { RandomizerDistribution.EngraveSecure, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceHpAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceStrAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceTechAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceQuickAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceLuckAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceDefAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceMagicAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceMdefAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhancePhysAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceMoveAlly, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceHpEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceStrEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceTechEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceQuickEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceLuckEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceDefEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceMagicEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceMdefEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhancePhysEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.SynchroEnhanceMoveEnemy, DataSetEnum.GodGeneral },
            { RandomizerDistribution.InheritanceSkills, DataSetEnum.GrowthTable },
            { RandomizerDistribution.SynchroStatSkillsAlly, DataSetEnum.GrowthTable },
            { RandomizerDistribution.SynchroStatSkillsEnemy, DataSetEnum.GrowthTable },
            { RandomizerDistribution.SynchroGeneralSkillsAlly, DataSetEnum.GrowthTable },
            { RandomizerDistribution.SynchroGeneralSkillsEnemy, DataSetEnum.GrowthTable },
            { RandomizerDistribution.EngageSkills, DataSetEnum.GrowthTable },
            { RandomizerDistribution.EngageItems, DataSetEnum.GrowthTable },
            { RandomizerDistribution.Aptitude, DataSetEnum.GrowthTable },
            { RandomizerDistribution.SkillInheritanceLevel, DataSetEnum.GrowthTable },
            { RandomizerDistribution.StrongBondLevel, DataSetEnum.GrowthTable },
            { RandomizerDistribution.DeepSynergyLevel, DataSetEnum.GrowthTable },
            { RandomizerDistribution.Exp, DataSetEnum.BondLevel },
            { RandomizerDistribution.Cost, DataSetEnum.BondLevel }
        };
    }

}
