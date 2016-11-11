
var i = 1;
function addAnswer()
{
    if(i <= 4)
    {
        i++;
        var div = document.createElement('div');
        div.innerHTML = 'Answer' + i + ' : <input type="text" name="answer_'+i+'" > <input type="button" id="addAnswer()" onClick="addAnswer()" value="+" /> <input type="button" value="-" onClick="removeAnswer(this)">';
        document.getElementById('AnswerOption').appendChild(div);
    }
}
function removeAnswer()
{
    document.getElementById('AnswerOption').removeChild( div.parentNode );
}

