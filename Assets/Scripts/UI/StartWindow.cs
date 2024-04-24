using HolyWater.MykytaTask.Infrastructure.Services.ProgressBar;
using HolyWater.MykytaTask.Infrastructure.StateMachine;
using HolyWater.MykytaTask.Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HolyWater.MykytaTask.UI
{
    public class StartWindow : MonoBehaviour
    {
        [SerializeField] private Button loadLevelButton;
        [SerializeField] private Image progressBar;
        
        private IGameStateMachine gameStateMachine;
        private IProgressBarService progressBarService;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine, IProgressBarService progressBarService)
        {
            this.progressBarService = progressBarService;
            this.gameStateMachine = gameStateMachine;
        }
        
        private void Start()
        {
            progressBarService.ProgressBarImage = progressBar;
            loadLevelButton.onClick.AddListener(OnSwitchState);
        }

        private void OnSwitchState() => 
            gameStateMachine.Enter<LoadProgressState>();
    }
}