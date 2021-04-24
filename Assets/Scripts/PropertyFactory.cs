using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyFactory : MonoBehaviour
{



    private void Start()
    {
        IncreasePlayerHealthProperty increasePlayerHealthProperty = new IncreasePlayerHealthProperty();
        ReducePlayerHealthProperty reducePlayerHealthProperty = new ReducePlayerHealthProperty();

        IncreasePlayerBulletsProperty increasePlayerBulletsProperty = new IncreasePlayerBulletsProperty();
        DecreasePlayerBulletsProperty decreasePlayerBulletsProperty = new DecreasePlayerBulletsProperty();

        IncreasePlayerGoodiesProperty increasePlayerGoodiesProperty = new IncreasePlayerGoodiesProperty();

        PlayerWinProperty playerWinProperty = new PlayerWinProperty();
        PlayerLostProperty playerLost = new PlayerLostProperty();
        PlayerGiveGunProperty playerGiveGunProperty = new PlayerGiveGunProperty();
    }

}


public enum EnumProperties
{
    None = 0,
    ReduceHealth,
    IncreaseHealth,
    DecreaseBullets,
    IncreaseBullets,
    IncreaseGoodies,
    PlayerWins,
    PlayerLost,
    GiveGun,
};


public abstract class CustomProperty
{
    public static Dictionary<EnumProperties, CustomProperty> customProperties;

    public CustomProperty()
    {
        if (customProperties == null) customProperties = new Dictionary<EnumProperties, CustomProperty>();

        customProperties.Add(propertyName, this);
    }

    public abstract EnumProperties propertyName { get; }

    public abstract void UpdateProperty();
    
}

public class ReducePlayerHealthProperty : CustomProperty
{
    public override EnumProperties propertyName => EnumProperties.ReduceHealth;

    public override void UpdateProperty()
    {
        if(ScoreManager.Instance)
        ScoreManager.Instance.UpdateHealth(--PlayerProperties.Instance.health);

        if (PlayerProperties.Instance.health <= 0)
        {
            CustomProperty.customProperties[EnumProperties.PlayerLost].UpdateProperty();
        }
    }
}


public class IncreasePlayerHealthProperty : CustomProperty
{
    public override EnumProperties propertyName => EnumProperties.IncreaseHealth;

    public override void UpdateProperty()
    {
        PlayerProperties.Instance.health++;

    }
}

public class DecreasePlayerBulletsProperty : CustomProperty
{
    public override EnumProperties propertyName => EnumProperties.DecreaseBullets;

    public override void UpdateProperty()
    {
        ScoreManager.Instance.UpdateBullets(--PlayerProperties.Instance.bullets);
    }
}

public class IncreasePlayerBulletsProperty : CustomProperty
{
    public override EnumProperties propertyName => EnumProperties.IncreaseBullets;

    public override void UpdateProperty()
    {
        PlayerProperties.Instance.health+=10;
        ScoreManager.Instance.UpdateBullets(PlayerProperties.Instance.bullets);

    }
}

public class IncreasePlayerGoodiesProperty : CustomProperty
{
    public override EnumProperties propertyName => EnumProperties.IncreaseGoodies;

    public override void UpdateProperty()
    {
        PlayerProperties.Instance.goodies+=20;
        ScoreManager.Instance.UpdateGoodies(PlayerProperties.Instance.goodies);

    }
}


public class PlayerWinProperty : CustomProperty
{
    public override EnumProperties propertyName => EnumProperties.PlayerWins;

    public override void UpdateProperty()
    {
        CustomProperty.customProperties[EnumProperties.IncreaseGoodies].UpdateProperty();
        CustomProperty.customProperties[EnumProperties.IncreaseGoodies].UpdateProperty();
        PlayerProperties.Instance.bHasGameWon = true;
        LevelManager.Instance.GameExitScene();
        UI_Manager.Instance.UpdateUI_State(UI_Manager.UI_States.GameExit);


        ScoreManager.Instance.UpdateScore();
        ScoreManager.Instance.UpdateHighScore();
        ScoreManager.Instance.SaveHighScore();

        AudioManager.Instance.PlayMusic(AudioManager.BGMusic_Enums.GameWin);
        ScoreManager.Instance.UpdateGameExitText(true);
        Physics.autoSimulation = false;

        Cursor.lockState =  CursorLockMode.None ;
    }
}

public class PlayerLostProperty : CustomProperty
{
    public override EnumProperties propertyName => EnumProperties.PlayerLost;

    public override void UpdateProperty()
    {
        PlayerProperties.Instance.bHasGameWon = false;
        LevelManager.Instance.GameExitScene();
        UI_Manager.Instance.UpdateUI_State(UI_Manager.UI_States.GameExit);
        Cursor.lockState =  CursorLockMode.None ;


        ScoreManager.Instance.UpdateScore();
        ScoreManager.Instance.UpdateHighScore();
        ScoreManager.Instance.SaveHighScore();
        AudioManager.Instance.PlayMusic(AudioManager.BGMusic_Enums.GameLost);

        ScoreManager.Instance.UpdateGameExitText(false);
    }
}

public class PlayerGiveGunProperty : CustomProperty
{
    public override EnumProperties propertyName => EnumProperties.GiveGun;

    public override void UpdateProperty()
    {
        PlayerProperties.Instance.bHasGun = true;
        PlayerProperties.Instance.PlayerControllerInstance.ActivateGun();
    }
}