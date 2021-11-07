import { AxeriApi } from "@axeridev/axeri-node-api/app";
import readline from "readline";

const axeri = new AxeriApi({
	port: 7090
});

axeri.onReady(() => {
	console.log("Bot Ready");

	axeri.account.onLoginTokenSuccess(() => {
		console.log("Logged in!");

		setTimeout(() => {
			axeri.guild.onJoinSuccess((id) => {
				console.log("Guild: Joined '" + id + "'");
			});

			axeri.guild.onJoinFailed((id, reason) => {
				console.log("Guild Error: Failed to join '" + id + "' for reason '" + reason + "'");
			});

			axeri.guild.joinGuild("g:1a");
		});
	});

	axeri.account.onLoginTokenFailed(() => {
		console.log("Failed to login");
	});

	axeri.account.onLoginGetTokenSuccess((token) => {
		axeri.account.loginToken(token);
	});

	axeri.account.onLoginGetTokenFailed((reason) => {
		console.log("Failed to login because reason '" + reason + "'");
	});

	const rl = readline.createInterface({
		input: process.stdin,
		output: process.stdout
	});

	let username = "";
	let password = "";

	axeri.transportToken.onCreateEndPointError((reason) => {
		console.log("Failed to create token transport endpoint for reason '" + reason + "'");
	});

	axeri.transportToken.onTokenResponse((token) => {
		console.log("Got token a called '" + token + "'");
	});

	axeri.transportToken.onCreateEndPointSuccess((id) => {
		console.log("Created endpoint with id '" + id + "'");

		axeri.transportToken.sendToEndpoint(id, "Hello Token");
	});

	axeri.transportToken.createEndPoint();
});

axeri.run();