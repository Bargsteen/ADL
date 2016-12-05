var textAssignmentCount = 0;
var ecAssignmentCount = 0;
var mcAssignmentCount = 0;
var ecAssignmentAnswerOptionCount = [];
var mcAssigmentAnswerOptionCount = [];


function addAssignment(assignmentType) {
    var allAssignmentsDiv = document.getElementById("allAssignmentsDiv");
    var assignmentDiv = document.createElement("div");
    var assignmentId;
    switch (assignmentType) {
        case "txt":

            assignmentId = textAssignmentCount++;
            createTextAssignment(assignmentId, assignmentDiv, "Text");
            break;
        case "mc":
            assignmentId = mcAssignmentCount++;
            mcAssigmentAnswerOptionCount[assignmentId] = 0;
            createChoiceAssignment(assignmentId, assignmentDiv, "MultipleChoice");
            break;
        case "ec":
            assignmentId = ecAssignmentCount++;
            ecAssignmentAnswerOptionCount[assignmentId] = 0;
            createChoiceAssignment(assignmentId, assignmentDiv, "ExclusiveChoice");
            break;
    }
    allAssignmentsDiv.appendChild(assignmentDiv);
}

function createTextAssignment(assignmentId, assignmentDiv, assignmentType) {

    var standardFields = addStandardFields(assignmentId, assignmentDiv, assignmentType);
    var commonEndings = commonDivEndings();
    var htmlContent = standardFields + commonEndings;

    assignmentDiv.innerHTML = htmlContent;
}

function createChoiceAssignment(assignmentId, assignmentDiv, assignmentType) {
    var htmlContent = "";
    var standardFields = addStandardFields(assignmentId, assignmentDiv, assignmentType);
    var answerOptionsDivId = assignmentDiv.id + '-Input-Group';
    var plusButtonFunction = 'addAnswerOption(' + assignmentId + ', \'' + answerOptionsDivId + '\', \'' + assignmentType + '\')';
    var minusButtonFunction = 'removeLastAnswerOption(' + assignmentId + ', \'' + answerOptionsDivId + '\', \'' + assignmentType + '\')';

    var buttonGroup = '<div class="input-group" id="' + answerOptionsDivId + '">' +
        '</div>' +
        '<div class="btn-group">' +
        '<button type="button" class="btn btn-default" onclick="' + plusButtonFunction + '"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span></button>' +
        '<button type="button" class="btn btn-default" onclick="' + minusButtonFunction + '"><span class="glyphicon glyphicon-minus" aria-hidden="true"></span></button>' +
        '</div>';

    htmlContent = standardFields + buttonGroup;
    assignmentDiv.innerHTML = htmlContent;

}



function addStandardFields(assignmentId, assignmentDiv, assignmentType) {
    var assignmentTypeEnumIndex;
    var panelColor;
    var panelTitleText;
    switch (assignmentType) {
        case "Text":
            assignmentTypeEnumIndex = 2;
            panelColor = "panel-info";
            panelTitleText = "Tekst-opgaver";
            break;
        case "MultipleChoice":
            assignmentTypeEnumIndex = 0;
            panelColor = "panel-warning";
            panelTitleText = "Multiplechoice-opgaver";
            break;
        case "ExclusiveChoice":
            assignmentTypeEnumIndex = 1;
            panelColor = "panel-success";
            panelTitleText = "Exclusivechoice-opgaver";
            break;
    }

    assignmentDiv.id = assignmentType + "Assignment-" + assignmentId;
    assignmentDiv.className = "col-md-6";

    var theId = assignmentType + 'Assignments_' + assignmentId + '__';
    var textareaId = theId + 'Text';
    var theName = assignmentType + 'Assignments[' + assignmentId + '].'
    var textareaName = theName + 'Text';
    var typeId = theId + 'Type';
    var typeName = theName + 'Type';

    return '<div class="panel ' + panelColor + '">' +
        '<div class="panel-heading">' +
        '<h3 class="panel-title">' + panelTitleText + '</h3>' +
        ' </div>' +
        ' <div class="panel-body">' +
        ' <div class="row">' +
        ' <div class="col-md-12" id="' + assignmentDiv.id + '-Input"' + '>' +
        '<div class="form-group row">' +
        '<input type="hidden" id="' + typeId + '" name="' + typeName + '" value=' + assignmentTypeEnumIndex + ' />' +
        '<label class="col-md-2 col-form-label">Tekst:</label>' +
        '<div class="col-md-10">' +
        '<textarea type="text" class="form-control" id="' + textareaId + '" name="' + textareaName + '" rows="6" /></textarea>' +
        '</div>' +
        '</div>';
}



function commonDivEndings() {
    return "</div></div></div></div>";
}




function addAnswerOption(assignmentId, theDivId, assignmentType) {
    theDiv = document.getElementById(theDivId);
    var isMultipleChoice = assignmentType === "MultipleChoice";
    var assignmentAnswerOptionCount = isMultipleChoice ? mcAssigmentAnswerOptionCount : ecAssignmentAnswerOptionCount;
    var currentAnswerOption = assignmentAnswerOptionCount[assignmentId]++;
    var answerOptionDivId = theDivId + "-A-" + currentAnswerOption;
    var inputFieldName = assignmentType + "Assignments[" + assignmentId + "].AnswerOptions[" + currentAnswerOption + "].Text";
    var inputFieldId = assignmentType + "Assignments_" + assignmentId + "__AnswerOptions_" + currentAnswerOption + "__Text";
    var helperText;
    var choiceHtml;


    if (isMultipleChoice) {
        var checkBoxName = assignmentType + "Assignments[" + assignmentId + "].AnswerCorrectness[" + currentAnswerOption + "].Value";
        var checkBoxId = assignmentType + "Assignments_" + assignmentId + "__AnswerCorrectness_" + currentAnswerOption + "__Value_";
        var hiddenFallBackInputCheck = '<input type="hidden" id="' + checkBoxId + '" name="' + checkBoxName + '" value="false" />';
        choiceHtml = '<div class="input-group"><span class="input-group-addon">' +
            '<input type="checkbox" id= "' + checkBoxId + '" name="' + checkBoxName + '" value="true" />' +
            hiddenFallBackInputCheck + '</span>';
        helperText = '<small class="form-text text-muted">Vælg hvilke svarmuligheder, der er rigtige:</small>';

    }
    else {
        var radioButtonName = assignmentType + "Assignments[" + assignmentId + "].CorrectAnswer";
        var radioButtonId = assignmentType + "Assignments_" + assignmentId + "__CorrectAnswer";
        choiceHtml = '<div class="input-group"><span class="input-group-addon">' +
            '<input type="radio" id="' + radioButtonId + '" name="' + radioButtonName + '" value="' + currentAnswerOption + '" /></span>';
        helperText = '<small class="form-text text-muted">Vælg hvilken svarmulighed, der er den rigtige:</small>';
    }

    
    var inputHtml = '<input type="text" class="form-control" id="' + inputFieldId + '" name="' + inputFieldName + '" />';

    var answerOptionDivOpen = '<div id="' + answerOptionDivId + '">';
    var answerOptionDivClose = "</div></div>";

    var htmlContent = answerOptionDivOpen;
    if (currentAnswerOption == 0) {
        htmlContent += helperText;
    }

    htmlContent += choiceHtml + inputHtml + answerOptionDivClose;

    theDiv.innerHTML += htmlContent;

}

function removeLastAnswerOption(assignmentId, theDivId, assignmentType) {
    var assignmentAnswerOptionCount = assignmentType === "MultipleChoice" ? mcAssigmentAnswerOptionCount : ecAssignmentAnswerOptionCount;
    if (assignmentAnswerOptionCount[assignmentId] > 0) {
        var lastAnswerOptionDivId = theDivId + "-A-" + --assignmentAnswerOptionCount[assignmentId];
        document.getElementById(lastAnswerOptionDivId).remove();
    }
}

