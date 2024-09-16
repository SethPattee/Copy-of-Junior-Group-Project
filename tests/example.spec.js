// @ts-check
const { test, expect } = require('@playwright/test');

test('has title', async ({ page }) => {
  await page.goto('https://playwright.dev/');

  // Expect a title "to contain" a substring.
  await expect(page).toHaveTitle(/Playwright/);
});

test('get started link', async ({ page }) => {
  await page.goto('https://playwright.dev/');

  // Click the get started link.
  await page.getByRole('link', { name: 'Get started' }).click();

  // Expects page to have a heading with the name of Installation.
  await expect(page.getByRole('heading', { name: 'Installation' })).toBeVisible();
});
////////////////////////////////////////////////////////////////////////////////////

// test('navagates home', async ({page}) => {
//   await page.goto('https://localhost:7237/form');

//   await page.dispatchEvent('a:contains("Home")', 'click');

//   await expect(page.url).toEqual('https://localhost:7237/');
// });

test('testing funcitonality', async ({ page }) => {
  await page.goto('https://localhost:7237/');
  await page.getByRole('link', { name: 'FAQ' }).click();
  await page.getByRole('link', { name: 'Services' }).click();
  await expect(page.getByRole('row', { name: 'Oil Change $50.00 Why do I' }).getByRole('button')).toBeVisible();
  await page.getByRole('link', { name: 'Request Service' }).click();
  await page.getByRole('button', { name: 'Confirm' }).click();
  await page.goto('https://localhost:7237/submit');
  await page.goto('https://localhost:7237/');
});

test('testing', async ({ page }) => {
  await page.goto('https://localhost:7237/');
  await page.getByRole('link', { name: 'FAQ' }).click();
  await page.getByRole('link', { name: 'Services' }).click();
  await page.getByRole('link', { name: 'Request Service' }).click();
  await page.getByRole('link', { name: 'Logo' }).click();
  const page1Promise = page.waitForEvent('popup');
  await page.getByRole('button', { name: 'Contact us!' }).click();
  const page1 = await page1Promise;
  await page.getByRole('link', { name: 'doc-fuelpump@gmail.com' }).click();
});