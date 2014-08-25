    function Update ()
    {
    //check if you character fell off the platform
    if(transform.position.y < -7)
    {
    transform.position.y = 1; //set the y axis that the player will spawn to
    transform.position.x = 49; // set the x axis that the player will spawn to
    transform.position.z = 45; // set the z axis that the player will spawn to
    Debug.Log("Respawn occured");
    }
    }