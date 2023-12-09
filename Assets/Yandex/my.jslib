mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
  },

  ShowAdv: function()
  {
YaGames.init().then(ysdk => ysdk.adv.showFullscreenAdv({
    callbacks: {
    	onOpen: () => {
          console.log('Video ad open.');
          myGameInstance.SendMessage("GameManager", "OpenAdv");
        },
        onClose: function(wasShown) {
      	  console.log('Video ad closed.');
          myGameInstance.SendMessage("GameManager", "CloseAdv");

        },
        onError: function(error) {
          // some action on error
        }
    }
}))

  },

  AddCoinsExtern: function(){
YaGames.init().then(ysdk => ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
          myGameInstance.SendMessage("GameManager", "OpenAdv");
        },
        onRewarded: () => {
          
        },
        onClose: () => {
          console.log('Video ad closed.');
          myGameInstance.SendMessage("GameManager", "CloseAdv");
          myGameInstance.SendMessage("GameManager", "AddCoins");
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
}))
  },

SetToLeaderBoard:function(value)
{
ysdk.getLeaderboards()
  .then(lb => {
    lb.setLeaderboardScore('TopScore', value);
  });
},

});