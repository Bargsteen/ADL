var assignmentCount = 0;

class Assignment {
    constructor(assignmentId) {
        this.assignmentId = assignmentId;
        this.assignmentDiv = document.createElement("div");
        this.assignmentDiv.textContent = "Assignment-" + this.assignmentId;
        this.assignmentDiv.id = "Assignment-" + this.assignmentId;
    }

    appendGeneralFields() {
        var assignmentText = document.createElement("input");
        assignmentText.type = "text";
        assignmentText.defaultValue = "Opgavetekst for opgave " + this.assignmentId;
        this.assignmentDiv.appendChild(assignmentText);
    }
}

class MultipleChoiceAssignment extends Assignment {
    constructor(assignmentId) {
        super(assignmentId); // call base/superclass constructor
        this.answerOptionCount = 0;
        this.answerOptionDiv = document.createElement("div");

    }

    appendMultipleChoiceFields() {
        this.appendGeneralFields();
        this.answerOptionDiv.id = "AnswerOptionDiv-" + this.assignmentId;
        this.answerOptionDiv.innerText = "AnswerOptionDiv-" + this.assignmentId;
        var plusButton = document.createElement("input");
        plusButton.value = "+";
        plusButton.type = "button";
        plusButton.onclick = function () {
            var newAnswerOption = document.createElement("input");
            newAnswerOption.type = "text";
            newAnswerOption.id = "AnswerOption-" + this.answerOptionCount;
            newAnswerOption.defaultValue = "Svarmulighed" + this.answerOptionCount;
            answerOptionsDiv.appendChild(newAnswerOption);
            this.answerOptionCount++;
        };
        this.assignmentDiv.appendChild(this.answerOptionDiv);
        this.assignmentDiv.appendChild(plusButton);

    }

    appendAnswerOption() {

    }

}



function addAssignment(assignmentType) {
    var assignmentsBox = document.getElementById("assignments-box");

    var newAssignment;

    if (assignmentType === 0) { // Text
        newAssignment = new Assignment(assignmentCount);
        newAssignment.appendGeneralFields();
    }
    else if (assignmentType === 1) { // Multiple Choice
        newAssignment = new MultipleChoiceAssignment(assignmentCount);
        newAssignment.appendMultipleChoiceFields();
        //appendMultipleChoiceAssignment(newAssignment.assignmentDiv);
    }
    else if (assignmentType === 2) { // Exclusive Choice
        //
    }

    assignmentsBox.appendChild(newAssignment.assignmentDiv);
    assignmentCount++;
}




