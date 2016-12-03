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
            createMultipleChoiceAssignment(assignmentId, assignmentDiv, "MultipleChoice");
            break;
        case "ec":
            assignmentId = ecAssignmentCount++;
            ecAssignmentAnswerOptionCount[assignmenId] = 0;
            //text.innerText  = "EC";
            //assignmentId = new ChoiceAssignment(ecAssignmentCount);
            //addStandardFields(assignmentId, assignmentDiv, "ExclusiveChoice");
            //addExclusiveChoiceFieldsToDiv(assignmentId, assignmentDiv, "ExclusiveChoice");
            break;
    }
    allAssignmentsDiv.appendChild(assignmentDiv);
    //allAssignmentsDiv.appendChild(text);
}

function addStandardFields(assignmentId, assignmentDiv, assignmentType) {

    var panelColor;
    var panelTitleText;
    switch (assignmentType) {
        case "Text":
            panelColor = "panel-info";
            panelTitleText = "Tekst-opgaver";
            break;
        case "MultipleChoice":
            panelColor = "panel-warning";
            panelTitleText = "Multiplechoice-opgaver";
            break;
        case "ExclusiveChoice":
            panelColor = "panel-success";
            panelTitleText = "Exclusivechoice-opgaver";
            break;
    }

    assignmentDiv.id = assignmentType + "Assignment-" + assignmentId;
    assignmentDiv.className = "col-md-6";

    var textareaId = assignmentType + 'Assignments_' + assignmentId + '__Text';
    var textareaName = assignmentType + 'Assignments[' + assignmentId + '].Text';

    return '<div class="panel ' + panelColor + '">' +
            '<div class="panel-heading">' +
                '<h3 class="panel-title">' + panelTitleText + '</h3>' +
            ' </div>' +
        ' <div class="panel-body">' +
            ' <div class="row">' +
                ' <div class="col-md-12" id="' + assignmentDiv.id + '-Input"' + '>' +
                    '<div class="form-group row">' +
                        '<label class="col-md-2 col-form-label">Tekst:</label>' +
                        '<div class="col-md-10">' +
        '<textarea type="text" class="form-control" id="' + textareaId + '" name="'+ textareaName + '" rows="6" /></textarea>' +
                        '</div>' +
        '</div>';
    



}

function createTextAssignment(assignmentId, assignmentDiv, assignmentType) {
    
    var standardFields = addStandardFields(assignmentId, assignmentDiv, assignmentType);
    var commonEndings = commonDivEndings();
    var htmlContent = standardFields + commonEndings;
    
    assignmentDiv.innerHTML = htmlContent;
}

function commonDivEndings() {
    return "</div></div></div></div>";
}


function createMultipleChoiceAssignment(assignmentId, assignmentDiv, assignmentType) {
    var htmlContent = "";
    var standardFields = addStandardFields(assignmentId, assignmentDiv, assignmentType);
    var answerOptionsDivId = assignmentDiv.id + '-Input-Group';
    var plusButtonFunction = 'addCheckBoxAndInputToDiv(' + assignmentId + ', \'' + answerOptionsDivId + '\', \'' + assignmentType + '\')';
    var minusButtonFunction = 'removeLastCheckBoxAndInputFromDiv(' + assignmentId + ', \'' + answerOptionsDivId + '\')';

    var buttonGroup = '<div class="input-group" id="' + answerOptionsDivId + '">' +
        '</div>' +
        '<div class="btn-group">' +
            '<button type="button" class="btn btn-default" onclick="' + plusButtonFunction + '"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span></button>' +
            '<button type="button" class="btn btn-default" onclick="' + minusButtonFunction + '"><span class="glyphicon glyphicon-minus" aria-hidden="true"></span></button>' +
        '</div>';

    htmlContent = standardFields + buttonGroup;
    assignmentDiv.innerHTML = htmlContent;
    
}


function addCheckBoxAndInputToDiv(assignmentId, theDivId, assignmentType) {
    theDiv = document.getElementById(theDivId);
    var currentAnswerOption = mcAssigmentAnswerOptionCount[assignmentId]++;
    var answerOptionDivId = theDivId + "-A-" + currentAnswerOption;
    var inputFieldName = assignmentType + "Assignments[" + assignmentId + "].AnswerOptions[" + currentAnswerOption + "].Text";
    var inputFieldId = assignmentType + "Assignments_" + assignmentId + "__AnswerOptions_" + currentAnswerOption + "__Text";
   
    var checkBoxName = assignmentType + "Assignments[" + assignmentId + "].AnswerCorrectness[" + currentAnswerOption + "]";
    var checkBoxId = assignmentType + "Assignments_" + assignmentId + "__AnswerCorrectness_" + currentAnswerOption + "_";
    var checkBoxValue = currentAnswerOption;

    var checkBoxHtml = '<div class="input-group"><span class="input-group-addon">' +
        '<input type="checkbox" id="' + checkBoxId + '" name="' + checkBoxName + '" value="false" />' +
        '</span>';
    var inputHtml = '<input type="text" class="form-control" id="' + inputFieldId + '" name="' + inputFieldName + '" />';

    var helperText = '<small class="form-text text-muted">Vælg hvilke svarmuligheder der er rigtige:</small>';

    var answerOptionDivOpen = '<div id="' + answerOptionDivId + '">';
    var answerOptionDivClose = "</div>";

    var htmlContent = answerOptionDivOpen;
    if (currentAnswerOption == 0)
    {
        htmlContent += helperText;
    }    
    htmlContent += checkBoxHtml + inputHtml + answerOptionDivClose;
    
    theDiv.innerHTML += htmlContent;
}

function removeLastCheckBoxAndInputFromDiv(assignmentId, theDivId) {
    if (mcAssigmentAnswerOptionCount[assignmentId] > 0) {
        var lastAnswerOptionDivId = theDivId + "-A-" + --mcAssigmentAnswerOptionCount[assignmentId];
        document.getElementById(lastAnswerOptionDivId).remove();
    }
}




/*
class Assignment {
    constructor(assignmentId) {
        this.assignmentId = assignmentId;
    }
}

class ChoiceAssignment extends Assignment {
    constructor(assignmentId) {
        super(assignmentId);
        this.answerOptionCount = 0;
    }
}




function addAssignment(assignmentType) {
    var allAssignmentsDiv = document.getElementById("allAssignmentsDiv");
    var newAssignmentDiv = document.createElement("div");
    var insertText;
    switch (assignmentType) {
        case "txt":
            newAssignment = new Assignment(textAssignmentCount);
            newAssignmentDiv.id = "TextAssignmentDiv-" + textAssignmentCount;
            addStandardFields(newAssignment, newAssignmentDiv, "Text");
            allAssignmentsDiv.appendChild(newAssignmentDiv);
            textAssignmentCount++;

            break;
        case "mc":
            newAssignment = new ChoiceAssignment(mcAssignmentCount);
            newAssignmentDiv.id = "MultipleChoiceAssignmentDiv-" + mcAssignmentCount;
            addStandardFields(newAssignment, newAssignmentDiv, "MultipleChoice");
            addMultipleChoiceFieldsToDiv(newAssignment, newAssignmentDiv);
            allAssignmentsDiv.appendChild(newAssignmentDiv);
            mcAssignmentCount++;
            break;
        case "ec":
            insertText = "ec";

    }
    var p = document.createElement("p");
    p.textContent = insertText;
    allAssignmentsDiv.appendChild(p);
}

function addStandardFields(assignment, theDiv, assignmentType) {
    var aType = document.createElement("input");
    aType.type = "hidden";
    aType.id = assignmentType + "Assignments_" + assignment.assignmentId + "__Type";
    aType.name = assignmentType + "Assignments[" + assignment.assignmentId + "].Type";
    aType.value = "2"; // text in enum

    var aText = document.createElement("input");
    aText.type = "text";
    aText.name = assignmentType + "Assignments[" + assignment.assignmentId + "].Text";
    aText.id = assignmentType + "Assignments_" + assignment.assignmentId + "__Text";

    theDiv.appendChild(aType);
    theDiv.appendChild(aText);
}

function addMultipleChoiceFieldsToDiv(assignment, theDiv) {
    var addButton = document.createElement("button");
    addButton.type = "button";
    addButton.textContent = "Tilføj";
    addButton.onclick = function() { addAnswerOptionToDiv(assignment, theDiv.id, "MultipleChoice") };

    var deleteButton = document.createElement("button");
    deleteButton.type = "button";
    deleteButton.textContent = "Fjern";
    deleteButton.onclick = function() { deleteAnswerOptionFromDiv(assignment, theDiv.id, "MultipleChoice") }
    theDiv.appendChild(addButton);
    theDiv.appendChild(deleteButton);
}

function addAnswerOptionToDiv(assignment, theDivId, assignmentType) {
    var theDiv = document.getElementById(theDivId);

    var answerOptionField = document.createElement("input");
    answerOptionField.name = assignmentType + "Assignments[" + assignment.assignmentId + "].AnswerOptions[" + assignment.answerOptionCount + "].Text";
    answerOptionField.id = assignmentType + "Assignments_" + assignment.assignmentId + "__AnswerOptions_ " + assignment.answerOptionCount + "__Text";

    assignment.answerOptionCount++;

    theDiv.appendChild(answerOptionField);
}

function deleteAnswerOptionFromDiv(assignment, theDivId, assignmentType) {

    if (assignment.answerOptionCount > 0) {
        var lastAnswerOptionId = assignmentType + "Assignments_" + assignment.assignmentId + "__AnswerOptions_ " + (assignment.answerOptionCount - 1) + "__Text";
        var lastAnswerOption = document.getElementById(lastAnswerOptionId);
        lastAnswerOption.parentNode.removeChild(lastAnswerOption);
        assignment.answerOptionCount--;
    }

}


*/

