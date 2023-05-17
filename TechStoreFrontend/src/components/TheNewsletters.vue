<template>
  <div class="newsletters">
    <div class="newsletters__heading">
      <h1>Subscribe to newsletter</h1>
      <h3>Lorem ipsum dolor sit.</h3>
    </div>
    <div class="newsletters__subscribe">
      <input type="text" placeholder="Enter yout email..." />
      <input type="submit" value="Subscribe" />
    </div>
  </div>
</template>

<style scoped>
.newsletters {
  background-color: var(--ts-c-bg-highlight);
  padding: 2rem;
  display: grid;
  gap: 5rem;
  grid-template-areas: "heading input";
}

.newsletters:nth-child(1) {
  grid-area: heading;
}

.newsletters:nth-child(2) {
  grid-area: input;
}

.newsletters__heading {
  color: var(--ts-c-text-dark);
  text-align: end;
}

.newsletters__heading h3 {
  color: var(--ts-c-primary-soft);
}

.newsletters__subscribe {
  position: relative;
  align-self: center;
  justify-self: start;
}

input[type="text"] {
  background-color: var(--ts-c-bg-light);
  border: none;
  border-radius: 2rem;
  outline: none;
  color: var(--ts-c-text-dark);
  font-weight: bold;
  padding-left: 1rem;
  height: 2rem;
  width: 40em;
}

input[type="submit"] {
  background-color: var(--ts-c-bg-dark);
  border: none;
  border-radius: 2rem;
  outline: none;
  color: var(--ts-c-text-light);
  font-weight: bold;
  height: 2rem;
  width: 7rem;
  position: absolute;
  right: 0;
}

@media only screen and (max-width: 1000px) {
  .newsletters {
    grid-template-areas:
      "heading"
      "input";
    gap: 1rem;
    justify-content: center;
    align-items: center;
  }

  .newsletters__heading {
    text-align: center;
  }

  .newsletters__subscribe,
  .newsletters__subscribe input {
    width: 100%;
  }
}
</style>
