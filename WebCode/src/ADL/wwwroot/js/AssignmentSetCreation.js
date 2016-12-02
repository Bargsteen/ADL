var assignmentCount = 0;



function addAssignment(assignmentType) {
    var allAssignmentsDiv = document.getElementById("assignments-box");
    var newAssignmentDiv = document.createElement("div");
    var insertText;
    switch (assignmentType)
    {
        case "txt":
            newAssignment = new Assignment(assignmentCount);
            addTextAssignmentFieldsToDiv(newAssignment, newAssignmentDiv);
            allAssignmentsDiv.appendChild(newAssignmentDiv);
            assignmentCount++;
            
            break;
        case "mc":
            insertText = "mc";
            break;
        case "ec":
            insertText = "ec";          
            
    }
    var p = document.createElement("p");
    p.textContent = insertText;
    allAssignmentsDiv.appendChild(p);
}


/*<input type="hidden" asp-for="Assignments[0].Type" value="2" />
                <input type="text" asp-for="Assignments[0].Title" />
                <input type="text" asp-for="Assignments[0].Text" />
                id="AnswerOptions_1__Text" name="AnswerOptions[1].Text"
                */

function addTextAssignmentFieldsToDiv(assignment, theDiv) {
    var aType = document.createElement("input");
    aType.type = "hidden";
    aType.id = "Assignments_" + assignment.assignmentId + "__Type";
    aType.name = "Assignments[" + assignment.assignmentId + "].Type";
    aType.value = "2"; // text in enum

    var aTitle = document.createElement("input");
    aTitle.type = "text";
    aTitle.name = "Assignments[" + assignment.assignmentId + "].Title";
    aTitle.id = "Assignments_" + assignment.assignmentId + "__Title";
    
    var aText = document.createElement("input");
    aText.type = "text";
    aText.name = "Assignments[" + assignment.assignmentId + "].Text";
    aText.id = "Assignments_" + assignment.assignmentId + "__Text";

    theDiv.appendChild(aType);
    theDiv.appendChild(aTitle);
    theDiv.appendChild(aText);
}


class Assignment{
    constructor(assignmentId)
    {
        this.assignmentId = assignmentId;
    }
}

class MultipleChoiceAssignment extends Assignment{
    constructor(assignmentId) {
        super(assignmentId);
        this.answerOptionCount = 0;
    }
}