var i = 2;
function addAnswer()
{
    if(i <= 5)
    {
        i++;
        var container = document.getElementById("answer-option-group");
        var label = document.createElement("label");
        var input = document.createElement("input");
        var removeButton = document.createElement("input");
       // var addButton = document.createElement("input");
        var div = document.createElement("div");
        
        input.type = "text";
        input.id = "AnswerOptions_" +(i-1)+"__text";
        input.name = "AnswerOptions["+(i-1)+"].Text";
        input.className = "form-control valid";  
        
        label.htmlFor = "AnswerOptions";
        label.innerText = "Svarmulighed "+i+":";    
        
        removeButton.type = "button";
        removeButton.onclick = function () {
            removeAnswer(div);
        }
        removeButton.value = "-"
        
        /*
        addButton.type = "button";
        addButton.onclick = addAnswer();
        addButton.id = "addAnswer()";
        addButton.value = "+"
        */
        div.appendChild(label);
        div.appendChild(input);
        //div.appendChild(addButton);
        div.appendChild(removeButton);
        
        container.appendChild(div);
    }
}
function removeAnswer(div)
{
 
}