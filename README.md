# 16-Windmills-SPAV
Die Windmühlen drehen sich jetzt einzeln, und die Buttons zum Locken funktionieren. Nach dem Locken dreht sich die nächste Windmühle. Am Ende werden alle Slider-Werte genommen und der Hintergrund wird gefärbt. Den Fehler habe ich jetzt behoben.

Was funktioniert jetzt:
  •	Einzeldrehung der Windmühlen:
      o	Alle Windmühlen drehen sich jetzt einzeln und nicht mehr gleichzeitig.
  •	Buttons zum Locken:
      o	Die Buttons funktionieren, um jede Windmühle einzeln zu locken.
      o	Sobald eine Windmühle gelocked ist, dreht sich die nächste.
  •	Farbänderung des Hintergrunds:
      o	Wenn alle Windmühlen gelocked sind, werden die Slider-Werte genommen.
      o	RGB-Werte werden kombiniert und damit der Hintergrund gefärbt.
  •	Keine Arrays mehr für Slider:
      o	Die Slider sind nicht mehr als Array festgelegt.
      o	Jeder Slider wird direkt aus der Windmühle geholt, ohne dass man ihn vorher im Array festlegen muss.
________________________________________
Was noch fehlt:
  •	Dynamische Erkennung der Windmühlen:
      o	Im Moment benutze ich noch ein windmills[]-Array.
      o	Ziel: Dass alle Windmühlen automatisch erkannt werden, egal wie viele ich in die Szene setze.
      o	Dann müsste man das Array nicht mehr manuell anpassen.



https://github.com/user-attachments/assets/164dc7e3-92c9-443c-81cd-8d4722df80eb

```mermaid
classDiagram

class GameManager {
    - GameObject[] windmills
    - GameObject colorCube
    - bool[] isLocked
    - int cIndex
    - float[] lockedSpeeds
    
    + Start() void
    + LockCurrentWindmill() void
    + Update() void
    + ApplyColorToCube() void
    + ActivateCurrentWindmill() void
}

class WindmillDynamicSpeed {
    - Light lampLight
    - float maxLightIntensity
    - Slider speedSlider
    - float maxRotationSpeed
    - float acceleration
    - float deceleration
    - float currentSpeed
    
    + Update() void
}

GameManager --> WindmillDynamicSpeed : uses
```

