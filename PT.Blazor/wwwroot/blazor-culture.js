window.locale = {
    get: () => localStorage.getItem("locale"),
    set: (value) => localStorage.setItem("locale", value),

    detect: () => {
        const lang = navigator.language || navigator.userLanguage;

        if (!lang)
            return "ru";

        return lang;
    }
};
