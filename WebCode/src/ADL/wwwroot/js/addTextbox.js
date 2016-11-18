var i = 2;
function addAnswer()
{
    if(i <= 4)
    {
        i++;
        var div = document.createElement('div');
        div.innerHTML = 'Answer ' + i + ' : <input class="form-control" id="AnswerOptions_'+i+'__Text" name="AnswerOption['+i+']"> <input type="button" id="addAnswer()" onClick="addAnswer()" value="+" /> <input type="button" value="-" onClick="removeAnswer(this)">';
        document.getElementById('AnswerOptionGroup').appendChild(div);
    }
}
function removeAnswer(div)
{
	i--;
    document.getElementById('AnswerOptionGroup').removeChild( div.parentNode );  
}