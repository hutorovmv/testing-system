$(document).ready(() => {
	if ($('#showActiveUser').length)
		$('#showActiveUser').submit();

	if ($('#settings').length)
		$('#settings').submit();

	if ($('#pagination-form').length)
		$('#pagination-form').submit();

	$('.horizontal-navigation-link').click(e => {
		$(".horizontal-navigation-link.active").removeClass("active");
		$(e.target).addClass("active");
	});

	$(document).on("click", ".page-item-prev", () => {
		let active = $(".active").find(".page-link").text();
		$("#page-index").val(1 + parseInt(active));
		$("#pagination-form").submit();
	});

	$(document).on("click", ".page-item-next", () => {
		let active = $(".active").find(".page-link").text();
		$("#page-index").val(1 + parseInt(active));
		$("#pagination-form").submit();
	});

	$(document).on("click", ".page-item", e => {
		let value = $(e.currentTarget).find(".page-link").text();
		$("#page-index").val(value);
		$("#pagination-form").submit();
	});

	$(document).on("mousedown", ".answer-checkbox", e => {
		let element = $(e.currentTarget);
		
		if (element.hasClass("checkbox-checked"))
			element.find("i").replaceWith('<i class="fa fa-square-o"></i>');
		else
			element.find("i").replaceWith('<i class="fa fa-check-square"></i>');

		element.addClass("checkbox-checked");
	});

	$(document).on("mousedown", ".answer-radio", e => {
		let element = $(e.currentTarget);
		let notSelectedIcon = '<i class="fa fa-circle-o"></i>';

		if (element.hasClass("radio-checked")) {
			element.find("i").replaceWith(notSelectedIcon);
		}
		else {
			element.parent(".answers").find("i").replaceWith(notSelectedIcon);
			element.find("i").replaceWith('<i class="fa fa-dot-circle-o"></i>');
		}

		element.toggleClass("radio-checked");
	});

	//$(document).on("click", ".delete", e => {
	//	let value = $(e.currentTarget).closest("tr").remove();
	//});
});

function readUrl(input) {
	if (input.files && input.files[0]) {
		let reader = new FileReader();
		reader.onload = e => $("#profileImage").attr("src", e.target.result);
		reader.readAsDataURL(input.files[0]);
	}
}

function startCountdown(totalSeconds) {
	let elementId = "#timer";
	let interval = setInterval(() => {
		if ($(elementId).length) {
			let time = calculateTime(totalSeconds);
			let timeStr = formatTime(time);

			$(elementId).text(`${timeStr.hours}:${timeStr.minutes}:${timeStr.seconds}`);
			totalSeconds--;

			if (totalSeconds < 0) {
				clearInterval(interval);
				$(elementId).text("EXPIRED");
				$("#end-testing").click();
			}
		}
	}, 1000);
}

function calculateTime(totalSeconds) {
	let hours = Math.floor(totalSeconds / 3600);
	totalSeconds %= 3600;
	let minutes = Math.floor(totalSeconds / 60);
	let seconds = totalSeconds % 60;

	return {
		hours: hours,
		minutes: minutes,
		seconds: seconds
	};
}

function formatTime(time) {
	return {
		hours: String(time.hours).padStart(2, '0'),
		minutes: String(time.minutes).padStart(2, '0'),
		seconds: String(time.seconds).padStart(2, '0')
	}
}

function submitQuestions() {
	$("#timer").attr("id", "timer-stopped");
	$("#timer-stopped").text("PASSED");
	$(".question-form").submit();
}

function answersPie(correct, wrong) {
	$(document).ready(() => {
        let pie = $("#chart")[0].getContext("2d");

		let answersPie = new Chart(pie, {
			type: "pie",
			data: {
				labels: ["Correct", "Not correct"],
				datasets: [{
					label: "Score",
                    data: [correct, wrong],
					backgroundColor: ["#0275d8", "#d9534f"],
					borderWidth: 0,
					hoverBorderWidth: 2,
					hoverBorderColor: "#292b2c"
				}]
			},
			options: {
				legend: {
					display: true,
					position: "bottom",
					labels: {
						fontColor: "000"
					}
				},
				tooltips: {
					enabled: true
				}
			}
		});
    });
}