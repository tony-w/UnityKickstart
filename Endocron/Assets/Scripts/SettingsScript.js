#pragma strict

function OnMouseEnter(){
	renderer.material.color = Color.grey;
}

function OnMouseExit(){
	renderer.material.color = Color.white;
}
