using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using UnityEngine.Events;

public class VideoPlayerWithSlider : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Drag VideoPlayer ke Inspector
    public Slider slider;            // Drag Slider UI ke Inspector
    public TextMeshProUGUI currentTimeText;     // (Optional) Drag Text UI untuk waktu saat ini
    public TextMeshProUGUI totalTimeText;       // (Optional) Drag Text UI untuk total durasi
    public UnityEvent onSliderFull;

    private bool isPrepared = false;
    private bool isDragging = false;

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.prepareCompleted += OnVideoPrepared;
            videoPlayer.Prepare();
        }

        if (slider != null)
        {
            // Tambahkan event listener untuk detect saat drag slider
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    void Update()
    {
        if (isPrepared && videoPlayer.isPlaying && !isDragging)
        {
            // Update slider value hanya kalau TIDAK sedang drag
            slider.value = (float)(videoPlayer.time / videoPlayer.length);

            // Update teks waktu saat ini
            if (currentTimeText != null)
            {
                currentTimeText.text = FormatTime(videoPlayer.time);
            }
        }

        if (slider.value > 0.9f)
        {
            onSliderFull?.Invoke();
        }

    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        isPrepared = true;

        // Set nilai slider
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = 0f;

        // Update total durasi
        if (totalTimeText != null)
        {
            totalTimeText.text = FormatTime(videoPlayer.length);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        if (isPrepared)
        {
            isDragging = true;
            double targetTime = value * videoPlayer.length;
            videoPlayer.time = targetTime;

            if (currentTimeText != null)
            {
                currentTimeText.text = FormatTime(targetTime);
            }

            isDragging = false;
        }
    }

    private string FormatTime(double timeInSeconds)
    {
        int minutes = Mathf.FloorToInt((float)timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt((float)timeInSeconds % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
