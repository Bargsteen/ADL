function addAssignment(assignmentType) {
    var assignmentsBox = document.getElementById("assignments-box");
    var assignment = document.createElement("div");
    assignment.className = "panel panel-default";


    var assignmentText = document.createElement("input");
    assignmentText.type = "text";
    assignmentText.className = "form-control";
    assignment.appendChild(assignmentText);

    var assignmentTitle = document.createElement("input");
    assignmentTitle.type = "text";
    assignmentTitle.className = "form-control";
    
    if (assignmentType === 0) { // Text
        
    }
    else if (assignmentType === 1) { // Multiple Choie
        var a = document.createElement("option");
        a.setAttribute("value", "volvocar");
        var b = document.createElement("option");
        b.setAttribute("value", "volasdvocar");
        var correctAnswerSelector = document.createElement("select");
        correctAnswerSelector.appendChild(a);
        correctAnswerSelector.appendChild(b);
        assignment.appendChild(correctAnswerSelector);
    }
    else if (assignmentType === 2) { // Exclusive Choice
        //
    }

    assignmentsBox.appendChild(assignment);
}