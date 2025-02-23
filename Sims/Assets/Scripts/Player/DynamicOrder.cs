using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicOrder : MonoBehaviour
{
    // 1. Check what room the player is currently in.
    // 2. Depending on the room, the player must adjust its "base order" in the layer.
    // 3. If the player is below the center point of the object, 
    //    they must appear in front of it. Otherwise, behind it.
    // 4. To achieve this, check using y-position of the objects present in the current room only.
    // 5. Only the player's order in layer must change.
}
