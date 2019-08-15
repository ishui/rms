// JavaScript Document
	function showmenu(){
		if (menu.style.display == ""){
			menu.style.display = "none";
			menubar.src = "../img/library/turn_right.gif";
		}
		else {
			menu.style.display = "";
			menubar.src = "../img/library/turn_left.gif";
		}
	}