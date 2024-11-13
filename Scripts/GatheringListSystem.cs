// <auto-generated>
using System;
using System.Data;
using UdonSharp;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VRC.SDK3.Data;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon;

namespace io.github.Azukimochi
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class GatheringListSystem : UdonSharpBehaviour
    {
        [SerializeField] private InputField _joinInfo;
        [SerializeField] private InputField _Discord;
        [SerializeField] private InputField _X;
        [SerializeField] private InputField _Tag;
        [SerializeField] private Text _Description;
        [SerializeField] private Toggle _toggle_Tech;
        [SerializeField] private Toggle _toggle_Academic;
        [SerializeField] private Text _timeText;

        [SerializeField] private GameObject _creditPanel;

        [SerializeField] public Color _defaultColor = Color.black;
        [SerializeField] public Color _selectedColor = Color.gray;
        [SerializeField] public Color _todayColor = new Color(0.3f, 0.3f, 0.3f);

        private void Update()
        {
            string time = GetJST().ToString("HH:mm:ss");

            time = $"現在時刻 {time} JST";
            _timeText.text = time;
        }

        private void Start()
        {
            _currentWeek = (Week)((int)GetJST().DayOfWeek + 1);
            Reflesh();
        }
    
        public void ToggleCredit()
        {
            _creditPanel.SetActive(!_creditPanel.activeSelf);
        }

        public void FilteringByAcademic()
        {
            Reflesh();
        }

        public void FilteringByTech()
        {
            Reflesh();
        }

        public void Reflesh()
        {
            var buttons = Buttons;
            foreach(var button in buttons)
                button.SetActive(false);
            foreach(var button in WeekButtons)
                button.GetComponentInChildren<Button>().image.color = _defaultColor;
            var todayWeek = (int)GetJST().DayOfWeek;
            WeekButtons[(int)GetJST().DayOfWeek].GetComponentInChildren<Button>().image.color = _todayColor;
            if (_currentWeek == Week.Sunday) { buttons = Buttons_Sun; WeekButtons[0].GetComponentInChildren<Button>().image.color = _selectedColor; } if (_currentWeek == Week.Monday) { buttons = Buttons_Mon; WeekButtons[1].GetComponentInChildren<Button>().image.color = _selectedColor; } if (_currentWeek == Week.Tuesday) { buttons = Buttons_Tue; WeekButtons[2].GetComponentInChildren<Button>().image.color = _selectedColor; } if (_currentWeek == Week.Wednesday) { buttons = Buttons_Wed; WeekButtons[3].GetComponentInChildren<Button>().image.color = _selectedColor; } if (_currentWeek == Week.Thursday) { buttons = Buttons_Thu; WeekButtons[4].GetComponentInChildren<Button>().image.color = _selectedColor; } if (_currentWeek == Week.Friday) { buttons = Buttons_Fri; WeekButtons[5].GetComponentInChildren<Button>().image.color = _selectedColor; } if (_currentWeek == Week.Saturday) { buttons = Buttons_Sat; WeekButtons[6].GetComponentInChildren<Button>().image.color = _selectedColor; } if (_currentWeek == Week.Other) { buttons = Buttons_Other; WeekButtons[7].GetComponentInChildren<Button>().image.color = _selectedColor; }
            foreach(var button in buttons)
                button.SetActive(true); 
            
            if (!_toggle_Tech.isOn)
                foreach (var x in Buttons_Tech)
                {
                    x.SetActive(false);
                }
            
            if (!_toggle_Academic.isOn)
                foreach (var x in Buttons_Academic)
                {
                    x.SetActive(false);
                }
        }

        public static DateTime GetJST()
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            var jstZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
#else
            var jstZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tokyo");
#endif
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, jstZoneInfo);
        }

        [SerializeField] public GameObject[] WeekButtons;
        [SerializeField] public GameObject[] Buttons;
        [SerializeField] public GameObject[] Buttons_Academic;
        [SerializeField] public GameObject[] Buttons_Tech;
        [SerializeField] public GameObject[] Buttons_Sun;
        [SerializeField] public GameObject[] Buttons_Mon;
        [SerializeField] public GameObject[] Buttons_Tue;
        [SerializeField] public GameObject[] Buttons_Wed;
        [SerializeField] public GameObject[] Buttons_Thu;
        [SerializeField] public GameObject[] Buttons_Fri;
        [SerializeField] public GameObject[] Buttons_Sat;
        [SerializeField] public GameObject[] Buttons_Other;

        private Week _currentWeek = Week.None;

        public void OnClicked_Sun() {_currentWeek = Week.Sunday;Reflesh();}
        public void OnClicked_Mon() {_currentWeek = Week.Monday;Reflesh();}
        public void OnClicked_Tue() {_currentWeek = Week.Tuesday;Reflesh();}
        public void OnClicked_Wed() {_currentWeek = Week.Wednesday;Reflesh();}
        public void OnClicked_Thu() {_currentWeek = Week.Thursday;Reflesh();}
        public void OnClicked_Fri() {_currentWeek = Week.Friday;Reflesh();}
        public void OnClicked_Sat() {_currentWeek = Week.Saturday;Reflesh();}
        public void OnClicked_Other() {_currentWeek = Week.Other;Reflesh();}


    public enum Week
    {
        None, 
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Other,
    } 
}
}