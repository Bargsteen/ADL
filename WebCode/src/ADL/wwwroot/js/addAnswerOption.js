function addAnswerOption(){
    var container = document.getElementById("answer-option-group");
    var input = document.createElement("input");
    input.type = "text";
    input.id = "AnswerOptions_0__Text";
    input.name = "AnswerOptions[0].Text";
    container.appendChild(input);
}