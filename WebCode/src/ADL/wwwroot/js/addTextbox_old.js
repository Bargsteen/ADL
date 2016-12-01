var i = 2;
function addAnswer()
{
    if(i <= 5)
    {
        i++;
       var div = document.createElement('div')
       div.innerHTML = '<label for="AnswerOptions">Svarmulighed '+i+':</label>' +
       '<input class="form-control" type="text" id="AnswerOptions_'+i+
       '__Text" name="AnswerOptions['+i+
       '].Text" value><input type="button" id="addAnswer()" onclick="addAnswer(this)" value="+"/>';
       document.getElementById('svarmuligheder').appendChild(div);
    }
}
function removeAnswer(div)
{
	i--;
    document.getElementById('AnswerOptionGroup').removeChild( div.parentNode );  
}