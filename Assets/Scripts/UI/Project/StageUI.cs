using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageUI : MonoBehaviour
{
    public static StageUI instance;

    [SerializeField] private LeftPanelUI leftPanel;

    [SerializeField] private Image stage1Img;
    [SerializeField] private Image stage2Img;
    [SerializeField] private Image stage3Img;

    [SerializeField] private Sprite inactiveStage;
    [SerializeField] private Sprite activeStage;

    [SerializeField] private TextMeshProUGUI workloadText;

    [SerializeField] private StageSliderUI slider1;
    [SerializeField] private StageSliderUI slider2;
    [SerializeField] private StageSliderUI slider3;
    [SerializeField] private StageSliderUI slider4;

    [SerializeField] private TextMeshProUGUI gameEngine;
    [SerializeField] private TextMeshProUGUI mainPlatformText;
    [SerializeField] private TextMeshProUGUI cpuText;
    [SerializeField] private TextMeshProUGUI gpuText;

    [SerializeField] private Image platformImage;

    [SerializeField] private ToggleGroup[] toggleGroups;
    [SerializeField] private FeatureToggle[] featurePanels;

    private GameProject game;

    private void Awake()
    {
        instance = this;
    }

    public void SetInfo(GameProject game)
    {
        leftPanel.SetInfo(game);

        if (game.currentStage.sliders is PrototypingSliders) SetStageImg(0);
        else if (game.currentStage.sliders is DevelopingSliders) SetStageImg(1);
        else if (game.currentStage.sliders is DesignSliders) SetStageImg(3);

        var slidersLocalization = game.currentStage.sliders.GetLocalizationKeys();
        slider1.SetText(slidersLocalization[0]);
        slider2.SetText(slidersLocalization[1]);
        slider3.SetText(slidersLocalization[2]);
        slider4.SetText(slidersLocalization[3]);
        slider1.ChangeValue(5);
        slider2.ChangeValue(5);
        slider3.ChangeValue(5);
        slider4.ChangeValue(5);

        gameEngine.text = game.engine.info.name;
        mainPlatformText.text = game.platforms[0].info.name;
        platformImage.sprite = game.platforms[0].info.sprite;

        this.game = game;

        UpdateDynamicTexts();
        UpdateFeatures();
    }

    private void SetStageImg(int stage)
    {
        stage1Img.sprite = stage == 0 ? activeStage : inactiveStage;
        stage2Img.sprite = stage == 1 ? activeStage : inactiveStage;
        stage3Img.sprite = stage == 2 ? activeStage : inactiveStage;
    }

    private void UpdateFeatures()
    {
        int currentPanel = 0;
        int currentGroup = 0;
        int stage = 0;
        if (game.currentStage.sliders is DevelopingSliders) stage = 1;
        if (game.currentStage.sliders is DesignSliders) stage = 2;
        for (int i = 0; i < AttributesManager.instance.features.Length; i++)
        {
            if (AttributesManager.instance.features[i].stage != (FeaturesGroup.Stage)stage) continue; 
            var featureGroup = AttributesManager.instance.features[i].GetEnabledFeatures(game.engine.info.features);
            if (featureGroup == null || featureGroup.Length == 0)  continue; 
            for (int f = 0; f < featureGroup.Length; f++)
            {
                featurePanels[currentPanel].gameObject.SetActive(true);
                featurePanels[currentPanel].SetInfo(featureGroup[f], toggleGroups[currentGroup]);
                currentPanel++;
            }
            currentGroup++;
        }
        for (int i = currentPanel; i < featurePanels.Length; i++)
        {
            featurePanels[i].gameObject.SetActive(false);
        }
    }

    public void UpdateDynamicTexts()
    {
        workloadText.text = game.currentStage.CalculateWorkload(game).ToString();

        int cpuUsage = game.currentStage.GetComputeUsage();
        int gpuUsage = game.currentStage.GetGraphicsUsage();
        int cpuAvailable = game.platforms[0].info.computeCapabilities;
        int gpuAvailable = game.platforms[0].info.graphicCapabilities;

        cpuText.text = cpuUsage + " / " + cpuAvailable;
        gpuText.text = gpuUsage + " / " + gpuAvailable;
        cpuText.color = cpuUsage > cpuAvailable? Color.red : Color.black;
        gpuText.color = gpuUsage > gpuAvailable ? Color.red : Color.black;
    }

    public void CancelProject()
    {
        ProjectManager.instance.project.Cancel();
    }

    public void Continue() // not sure, that i need this void 
    {
        //
    }

    /*
    private void Start() // used for tests only
    {
        ProjectManager.instance.project = new Game();
        var game = ProjectManager.instance.project as Game;
        game.projectName = "Super bro 3d";
        game.engine = AttributesManager.instance.engines[4];
        game.platforms[0] = AttributesManager.instance.platforms[10];
        game.size = 1;
        game.genre = AttributesManager.instance.genres[3];
        game.theme = AttributesManager.instance.themes[5];
        game.currentStage = new Stage(new DevelopingSliders());

        SetInfo(game);
    }
    */
}